using DargwaQuiz.Enums;

namespace DargwaQuiz.Data.Seeding;

public sealed class CategorySeedItem
{
    public string Name { get; init; } = string.Empty;
    public string? NameDargwa { get; init; }
    public string Description { get; init; } = string.Empty;
    public List<QuestionSeedItem> Questions { get; init; } = new();
}

public sealed class QuestionSeedItem
{
    public string Text { get; init; } = string.Empty;
    public QuestionDifficulty Difficulty { get; init; }
    public List<AnswerSeedItem> Answers { get; init; } = new();
}

public sealed class AnswerSeedItem
{
    public string Text { get; init; } = string.Empty;
    public bool IsCorrect { get; init; }
}