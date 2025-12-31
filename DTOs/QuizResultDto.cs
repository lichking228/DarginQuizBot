namespace DargwaQuiz.DTOs;

public class QuizResultDto
{
    public int QuizSessionId { get; set; }
    public int TotalQuestions { get; set; }
    public int CorrectAnswers { get; set; }
    public int Score { get; set; }
    public double AccuracyPercentage { get; set; }
    public TimeSpan Duration { get; set; }
    public string? CategoryName { get; set; }
}