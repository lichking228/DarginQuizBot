using DargwaQuiz.Data;
using DargwaQuiz.DTOs;
using DargwaQuiz.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DargwaQuiz.Services.Implementations;

public class StatisticsService : IStatisticsService
{
    private readonly QuizDbContext _context;

    public StatisticsService(QuizDbContext context)
    {
        _context = context;
    }

    public async Task<UserStatsDto> GetUserStatisticsAsync(long telegramId)
    {
        var user = await _context.Users
            .Include(u => u.UserAnswers)
            .FirstOrDefaultAsync(u => u.TelegramId == telegramId);

        if (user == null)
        {
            return new UserStatsDto { Username = "Unknown" };
        }

        return CalculateStats(user);
    }

    public async Task<List<UserStatsDto>> GetLeaderboardAsync(int topCount = 10)
    {
        var topUsers = await _context.Users
            .Include(u => u.UserAnswers)
            .OrderByDescending(u => u.TotalScore)
            .Take(topCount)
            .ToListAsync();

        var result = new List<UserStatsDto>();
        
        foreach (var user in topUsers)
        {
            result.Add(CalculateStats(user));
        }

        return result;
    }

    public async Task<QuizResultDto> GetQuizResultAsync(int quizSessionId)
    {
        var session = await _context.QuizSessions
            .Include(s => s.Category)
            .Include(s => s.UserAnswers)
            .FirstOrDefaultAsync(s => s.Id == quizSessionId);

        if (session == null)
        {
            return new QuizResultDto();
        }

        var duration = session.CompletedAt.HasValue 
            ? session.CompletedAt.Value - session.StartedAt 
            : TimeSpan.Zero;

        double accuracy = 0;
        if (session.TotalQuestions > 0)
        {
            accuracy = Math.Round((double)session.CorrectAnswers / session.TotalQuestions * 100, 2);
        }

        return new QuizResultDto
        {
            QuizSessionId = session.Id,
            TotalQuestions = session.TotalQuestions,
            CorrectAnswers = session.CorrectAnswers,
            Score = session.Score,
            AccuracyPercentage = accuracy,
            Duration = duration,
            CategoryName = session.Category?.Name
        };
    }

    private UserStatsDto CalculateStats(Models.User user)
    {
        var answers = user.UserAnswers ?? new List<Models.UserAnswer>();
        
        int totalAnswers = answers.Count;
        int correctAnswers = answers.Count(ua => ua.IsCorrect);
        
        double avgScore = 0;
        if (user.QuizzesCompleted > 0)
        {
            avgScore = (double)user.TotalScore / user.QuizzesCompleted;
        }

        double accuracy = 0;
        if (totalAnswers > 0)
        {
            accuracy = Math.Round((double)correctAnswers / totalAnswers * 100, 2);
        }

        return new UserStatsDto
        {
            Username = !string.IsNullOrEmpty(user.Username) && user.Username != "Unknown"
                ? user.Username 
                : (user.FirstName ?? "Аноним"),
            TotalScore = user.TotalScore,
            QuizzesCompleted = user.QuizzesCompleted,
            AverageScore = avgScore,
            TotalQuestionsAnswered = totalAnswers,
            CorrectAnswers = correctAnswers,
            AccuracyPercentage = accuracy
        };
    }
}
