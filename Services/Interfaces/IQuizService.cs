using DargwaQuiz.Models;

namespace DargwaQuiz.Services.Interfaces;

public interface IQuizService
{
    Task<QuizSession> CreateQuizSessionAsync(int userId, int? categoryId = null, int questionsCount = 10);
    Task<Question?> GetNextQuestionAsync(int quizSessionId);
    Task<bool> SubmitAnswerAsync(int quizSessionId, int questionId, int answerId, int timeSpentSeconds);
    
    Task<string> GetCorrectAnswerTextAsync(int questionId);

    Task<QuizSession?> GetActiveQuizSessionAsync(int userId);
    Task<QuizSession?> CompleteQuizSessionAsync(int quizSessionId);
    Task<List<Category>> GetAllCategoriesAsync();
}