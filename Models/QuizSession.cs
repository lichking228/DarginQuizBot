using DargwaQuiz.Enums;

namespace DargwaQuiz.Models;

public class QuizSession
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int? CategoryId { get; set; }
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
    public QuizStatus Status { get; set; } = QuizStatus.InProgress;
    public int TotalQuestions { get; set; }
    public int CorrectAnswers { get; set; } = 0;
    public int Score { get; set; } = 0;
    
    // Навигационные свойства
    public User User { get; set; } = null!;
    public Category? Category { get; set; }
    public ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
}