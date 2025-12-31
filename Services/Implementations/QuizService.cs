using DargwaQuiz.Data;
using DargwaQuiz.Enums;
using DargwaQuiz.Models;
using DargwaQuiz.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DargwaQuiz.Services.Implementations;

public class QuizService : IQuizService
{
    private readonly QuizDbContext _context;

    public QuizService(QuizDbContext context)
    {
        _context = context;
    }

    public async Task<QuizSession> CreateQuizSessionAsync(int userId, int? categoryId = null, int questionsCount = 10)
    {
        var session = new QuizSession
        {
            UserId = userId,
            CategoryId = categoryId,
            StartedAt = DateTime.UtcNow,
            Status = QuizStatus.InProgress,
            TotalQuestions = questionsCount
        };

        _context.QuizSessions.Add(session);
        await _context.SaveChangesAsync();

        return session;
    }

    public async Task<Question?> GetNextQuestionAsync(int quizSessionId)
    {
        var session = await _context.QuizSessions
            .Include(s => s.UserAnswers)
            .FirstOrDefaultAsync(s => s.Id == quizSessionId);

        if (session == null || session.Status != QuizStatus.InProgress)
            return null;

        var answeredQuestionIds = session.UserAnswers.Select(ua => ua.QuestionId).ToList();

        var query = _context.Questions
            .Include(q => q.Answers)
            .Where(q => q.IsActive && !answeredQuestionIds.Contains(q.Id));

        if (session.CategoryId.HasValue)
        {
            query = query.Where(q => q.CategoryId == session.CategoryId.Value);
        }

        return await query.OrderBy(q => Guid.NewGuid()).FirstOrDefaultAsync();
    }

    public async Task<bool> SubmitAnswerAsync(int quizSessionId, int questionId, int answerId, int timeSpentSeconds)
    {
        var session = await _context.QuizSessions.FindAsync(quizSessionId);
        if (session == null) return false;

        var answer = await _context.Answers
            .Include(a => a.Question)
            .FirstOrDefaultAsync(a => a.Id == answerId && a.QuestionId == questionId);

        if (answer == null) return false;

        var userAnswer = new UserAnswer
        {
            UserId = session.UserId,
            QuestionId = questionId,
            AnswerId = answerId,
            QuizSessionId = quizSessionId,
            IsCorrect = answer.IsCorrect,
            AnsweredAt = DateTime.UtcNow,
            TimeSpentSeconds = timeSpentSeconds
        };

        _context.UserAnswers.Add(userAnswer);

        if (answer.IsCorrect)
        {
            session.CorrectAnswers++;
            session.Score += CalculateScore(answer.Question.Difficulty, timeSpentSeconds);
        }

        var question = answer.Question;
        question.TimesAsked++;
        if (answer.IsCorrect)
        {
            question.TimesAnsweredCorrectly++;
        }

        await _context.SaveChangesAsync();

        return answer.IsCorrect;
    }

    public async Task<string> GetCorrectAnswerTextAsync(int questionId)
    {
        var text = await _context.Answers
            .Where(a => a.QuestionId == questionId && a.IsCorrect)
            .Select(a => a.Text)
            .FirstOrDefaultAsync();
        
        return text ?? "Неизвестно";
    }

    public async Task<QuizSession?> GetActiveQuizSessionAsync(int userId)
    {
        return await _context.QuizSessions
            .Include(s => s.UserAnswers)
            .FirstOrDefaultAsync(s => s.UserId == userId && s.Status == QuizStatus.InProgress);
    }

    public async Task<QuizSession?> CompleteQuizSessionAsync(int quizSessionId)
    {
        var session = await _context.QuizSessions
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.Id == quizSessionId);

        if (session == null) return null;

        session.Status = QuizStatus.Completed;
        session.CompletedAt = DateTime.UtcNow;

        if (session.User != null)
        {
            session.User.TotalScore += session.Score;
            session.User.TotalGames++;
        }

        await _context.SaveChangesAsync();

        return session;
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    private int CalculateScore(QuestionDifficulty difficulty, int timeSpent)
    {
        int baseScore = difficulty switch
        {
            QuestionDifficulty.Easy => 10,
            QuestionDifficulty.Medium => 20,
            QuestionDifficulty.Hard => 30,
            _ => 10
        };

        int speedBonus = timeSpent < 10 ? baseScore / 2 : 
                        timeSpent < 20 ? baseScore / 4 : 0;

        return baseScore + speedBonus;
    }
}
