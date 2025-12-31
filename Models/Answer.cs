namespace DargwaQuiz.Models;

public class Answer
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string Text { get; set; } = string.Empty;
    public string? TextDargwa { get; set; }
    public bool IsCorrect { get; set; } = false;
    public int OrderIndex { get; set; } = 0;
    
    // Навигационные свойства
    public Question Question { get; set; } = null!;
    public ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
}