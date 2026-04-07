using DargwaQuiz.Enums;

namespace DargwaQuiz.Data.Seeding;

public static class CategoriesSeedCatalog
{
    public static List<CategorySeedItem> Build() => new()
    {
        new CategorySeedItem
        {
            Name = "Основы",
            NameDargwa = "Основы",
            Description = "Базовые слова",
            Questions = new()
            {
                new QuestionSeedItem
                {
                    Text = "Салам",
                    Difficulty = QuestionDifficulty.Easy,
                    Answers = new()
                    {
                        new AnswerSeedItem { Text = "Привет", IsCorrect = true },
                        new AnswerSeedItem { Text = "Пока", IsCorrect = false },
                        new AnswerSeedItem { Text = "Идти", IsCorrect = false }
                    }
                },
                new QuestionSeedItem
                {
                    Text = "ХIу",
                    Difficulty = QuestionDifficulty.Easy,
                    Answers = new()
                    {
                        new AnswerSeedItem { Text = "Я", IsCorrect = false },
                        new AnswerSeedItem { Text = "Ты", IsCorrect = true },
                        new AnswerSeedItem { Text = "Мы", IsCorrect = false }
                    }
                }
            }
        },
    };
}