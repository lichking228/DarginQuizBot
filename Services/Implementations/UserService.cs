using DargwaQuiz.Data;
using DargwaQuiz.Enums;
using DargwaQuiz.Models;
using DargwaQuiz.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DargwaQuiz.Services.Implementations;

public class UserService : IUserService
{
    private readonly QuizDbContext _context;

    public UserService(QuizDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByTelegramIdAsync(long telegramId)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.TelegramId == telegramId);
    }

    public async Task<User> CreateUserAsync(long telegramId, string username, string? firstName, string? lastName)
    {
        var user = new User
        {
            TelegramId = telegramId,
            Username = username,
            FirstName = firstName,
            LastName = lastName,
            RegisteredAt = DateTime.UtcNow,
            LastActiveAt = DateTime.UtcNow,
            PreferredLanguage = UserLanguage.Russian // По умолчанию русский
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        
        return user;
    }

    public async Task UpdateLastActiveAsync(long telegramId)
    {
        var user = await GetUserByTelegramIdAsync(telegramId);
        if (user != null)
        {
            user.LastActiveAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> GetUserTotalScoreAsync(long telegramId)
    {
        var user = await GetUserByTelegramIdAsync(telegramId);
        return user?.TotalScore ?? 0;
    }

    public async Task UpdateUserScoreAsync(long telegramId, int scoreToAdd)
    {
        var user = await GetUserByTelegramIdAsync(telegramId);
        if (user != null)
        {
            user.TotalScore += scoreToAdd;
            user.QuizzesCompleted++;
            await _context.SaveChangesAsync();
        }
    }

    public async Task SetPreferredLanguageAsync(long telegramId, UserLanguage language)
    {
        var user = await GetUserByTelegramIdAsync(telegramId);
        if (user == null) return;

        user.PreferredLanguage = language;
        await _context.SaveChangesAsync();
    }

    public async Task<UserLanguage> GetPreferredLanguageAsync(long telegramId)
    {
        var user = await GetUserByTelegramIdAsync(telegramId);
        return user?.PreferredLanguage ?? UserLanguage.Russian;
    }
}
