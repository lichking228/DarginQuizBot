using DargwaQuiz.Enums;

namespace DargwaQuiz.Models;

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string? TextDargwa { get; set; }
    public string? Explanation { get; set; }
    public QuestionDifficulty Difficulty { get; set; } = QuestionDifficulty.Medium;
    public int CategoryId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
    public int TimesAsked { get; set; } = 0;
    public int TimesAnsweredCorrectly { get; set; } = 0;
    
    // Навигационные свойства
    public Category Category { get; set; } = null!;
    public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    public ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
}