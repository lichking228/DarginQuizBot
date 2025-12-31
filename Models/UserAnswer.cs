namespace DargwaQuiz.Models;

public class UserAnswer
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int QuestionId { get; set; }
    public int AnswerId { get; set; }
    public int? QuizSessionId { get; set; }
    public bool IsCorrect { get; set; }
    public DateTime AnsweredAt { get; set; } = DateTime.UtcNow;
    public int TimeSpentSeconds { get; set; }
    
    // Навигационные свойства
    public User User { get; set; } = null!;
    public Question Question { get; set; } = null!;
    public Answer Answer { get; set; } = null!;
    public QuizSession? QuizSession { get; set; }
}