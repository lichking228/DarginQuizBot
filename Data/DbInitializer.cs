using DargwaQuiz.Models;
using DargwaQuiz.Enums;

namespace DargwaQuiz.Data;

public static class DbInitializer
{
    public static void Initialize(QuizDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Categories.Any()) return;

        var catBasic = new Category 
        { 
            Name = "Основы", 
            NameDargwa = "Основы", 
            Description = "Базовые слова" 
        };

        context.Categories.Add(catBasic);
        context.SaveChanges();

        var questions = new List<Question>
        {
            new Question
            {
                CategoryId = catBasic.Id,
                Difficulty = QuestionDifficulty.Easy,
                Text = "Салам", 
                IsActive = true,
                Answers = new List<Answer>
                {
                    new Answer { Text = "Привет", IsCorrect = true },
                    new Answer { Text = "Пока", IsCorrect = false },
                    new Answer { Text = "Спасибо", IsCorrect = false }
                }
            },
            new Question
            {
                CategoryId = catBasic.Id,
                Difficulty = QuestionDifficulty.Easy,
                Text = "Баркаллагъ", 
                IsActive = true,
                Answers = new List<Answer>
                {
                    new Answer { Text = "Спасибо", IsCorrect = true },
                    new Answer { Text = "Пожалуйста", IsCorrect = false },
                    new Answer { Text = "Извини", IsCorrect = false }
                }
            },
            new Question
            {
                CategoryId = catBasic.Id,
                Difficulty = QuestionDifficulty.Easy,
                Text = "Удзи", 
                IsActive = true,
                Answers = new List<Answer>
                {
                    new Answer { Text = "Брат", IsCorrect = true },
                    new Answer { Text = "Сестра", IsCorrect = false },
                    new Answer { Text = "Отец", IsCorrect = false }
                }
            },
            new Question
            {
                CategoryId = catBasic.Id,
                Difficulty = QuestionDifficulty.Easy,
                Text = "Рудзи", 
                IsActive = true,
                Answers = new List<Answer>
                {
                    new Answer { Text = "Сестра", IsCorrect = true },
                    new Answer { Text = "Брат", IsCorrect = false },
                    new Answer { Text = "Мама", IsCorrect = false }
                }
            },
            new Question
            {
                CategoryId = catBasic.Id,
                Difficulty = QuestionDifficulty.Easy,
                Text = "Къурдаш", 
                IsActive = true,
                Answers = new List<Answer>
                {
                    new Answer { Text = "Друг", IsCorrect = true },
                    new Answer { Text = "Враг", IsCorrect = false },
                    new Answer { Text = "Сосед", IsCorrect = false }
                }
            },
            new Question
            {
                CategoryId = catBasic.Id,
                Difficulty = QuestionDifficulty.Medium,
                Text = "Азаддеш", 
                IsActive = true,
                Answers = new List<Answer>
                {
                    new Answer { Text = "Свобода", IsCorrect = true },
                    new Answer { Text = "Мир", IsCorrect = false },
                    new Answer { Text = "Жизнь", IsCorrect = false }
                }
            },
            new Question
            {
                CategoryId = catBasic.Id,
                Difficulty = QuestionDifficulty.Easy,
                Text = "КьацI", 
                IsActive = true,
                Answers = new List<Answer>
                {
                    new Answer { Text = "Хлеб", IsCorrect = true },
                    new Answer { Text = "Вода", IsCorrect = false },
                    new Answer { Text = "Мясо", IsCorrect = false }
                }
            },
            new Question
            {
                CategoryId = catBasic.Id,
                Difficulty = QuestionDifficulty.Easy,
                Text = "Някъ", 
                IsActive = true,
                Answers = new List<Answer>
                {
                    new Answer { Text = "Рука", IsCorrect = true },
                    new Answer { Text = "Нога", IsCorrect = false },
                    new Answer { Text = "Голова", IsCorrect = false }
                }
            },
            new Question
            {
                CategoryId = catBasic.Id,
                Difficulty = QuestionDifficulty.Easy,
                Text = "Шин", 
                IsActive = true,
                Answers = new List<Answer>
                {
                    new Answer { Text = "Вода", IsCorrect = true },
                    new Answer { Text = "Огонь", IsCorrect = false },
                    new Answer { Text = "Земля", IsCorrect = false }
                }
            },
            new Question
            {
                CategoryId = catBasic.Id,
                Difficulty = QuestionDifficulty.Easy,
                Text = "ГIинц", 
                IsActive = true,
                Answers = new List<Answer>
                {
                    new Answer { Text = "Яблоко", IsCorrect = true },
                    new Answer { Text = "Груша", IsCorrect = false },
                    new Answer { Text = "Персик", IsCorrect = false }
                }
            }
        };

        context.Questions.AddRange(questions);
        context.SaveChanges();
    }
}
