namespace DargwaQuiz.Models;

public class Answer
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; } = false;
    
    public Question Question { get; set; } = null!;
    public ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
}