using DargwaQuiz.Enums;
using DargwaQuiz.Models;

namespace DargwaQuiz.Services.Interfaces;

public interface IUserService
{
    Task<User?> GetUserByTelegramIdAsync(long telegramId);
    Task<User> CreateUserAsync(long telegramId, string username, string? firstName, string? lastName);
    Task UpdateLastActiveAsync(long telegramId);
    Task<int> GetUserTotalScoreAsync(long telegramId);
    Task UpdateUserScoreAsync(long telegramId, int scoreToAdd);
    
    Task SetPreferredLanguageAsync(long telegramId, UserLanguage language);
    Task<UserLanguage> GetPreferredLanguageAsync(long telegramId);
}