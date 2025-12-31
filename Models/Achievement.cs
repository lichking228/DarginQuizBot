namespace DargwaQuiz.Models;

public class Achievement
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = "🏆";
    public int RequiredScore { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Навигационные свойства
    public ICollection<User> Users { get; set; } = new List<User>();
}