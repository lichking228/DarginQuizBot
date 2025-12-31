namespace DargwaQuiz.DTOs;

public class UserStatsDto
{
    public string Username { get; set; } = string.Empty;
    public int TotalScore { get; set; }
    public int QuizzesCompleted { get; set; }
    public double AverageScore { get; set; }
    public int TotalQuestionsAnswered { get; set; }
    public int CorrectAnswers { get; set; }
    public double AccuracyPercentage { get; set; }
}