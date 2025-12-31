using DargwaQuiz.DTOs;

namespace DargwaQuiz.Services.Interfaces;

public interface IStatisticsService
{
    Task<UserStatsDto> GetUserStatisticsAsync(long telegramId);
    Task<List<UserStatsDto>> GetLeaderboardAsync(int topCount = 10);
    Task<QuizResultDto> GetQuizResultAsync(int quizSessionId);
}