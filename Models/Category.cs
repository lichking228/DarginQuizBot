namespace DargwaQuiz.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? NameDargwa { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Навигационные свойства
    public ICollection<Question> Questions { get; set; } = new List<Question>();
}