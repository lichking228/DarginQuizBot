namespace DargwaQuiz.DTOs;

public class QuestionDto
{
    public int QuestionId { get; set; }
    public string Text { get; set; } = string.Empty;
    public string? TextDargwa { get; set; }
    public List<AnswerOptionDto> Answers { get; set; } = new();
}

public class AnswerOptionDto
{
    public int AnswerId { get; set; }
    public string Text { get; set; } = string.Empty;
    public string? TextDargwa { get; set; }
}