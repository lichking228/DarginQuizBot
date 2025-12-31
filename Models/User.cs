using DargwaQuiz.Enums;

namespace DargwaQuiz.Models;

public class User
{
    public int Id { get; set; }
    public long TelegramId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    public DateTime LastActiveAt { get; set; } = DateTime.UtcNow;
    public int TotalScore { get; set; } = 0;
    public int QuizzesCompleted { get; set; } = 0;
    public UserLanguage PreferredLanguage { get; set; } = UserLanguage.Russian;
    public UserLanguage AddPreferredLanguage { get; set; } = UserLanguage.Dargwa;
    public int TotalGames { get; set; }


    // Навигационные свойства
    public ICollection<QuizSession> QuizSessions { get; set; } = new List<QuizSession>();
    public ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
    public ICollection<Achievement> Achievements { get; set; } = new List<Achievement>();
}