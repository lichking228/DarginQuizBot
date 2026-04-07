﻿using DargwaQuiz.Enums;

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
                },
                new QuestionSeedItem
                {
                    Text = "Ну",
                    Difficulty = QuestionDifficulty.Easy,
                    Answers = new()
                    {
                        new AnswerSeedItem { Text = "Он", IsCorrect = false },
                        new AnswerSeedItem { Text = "Я", IsCorrect = true },
                        new AnswerSeedItem { Text = "Они", IsCorrect = false }
                    }
                },
                new QuestionSeedItem
                {
                    Text = "Нуша",
                    Difficulty = QuestionDifficulty.Medium,
                    Answers = new()
                    {
                        new AnswerSeedItem { Text = "Его", IsCorrect = false },
                        new AnswerSeedItem { Text = "Их", IsCorrect = false },
                        new AnswerSeedItem { Text = "Мы", IsCorrect = true }
                    }
                },
                new QuestionSeedItem
                {
                    Text = "Чи?",
                    Difficulty = QuestionDifficulty.Medium,
                    Answers = new()
                    {
                        new AnswerSeedItem { Text = "Где?", IsCorrect = false },
                        new AnswerSeedItem { Text = "Кто?", IsCorrect = true },
                        new AnswerSeedItem { Text = "Когда?", IsCorrect = false }
                    }
                },
                new QuestionSeedItem
                {
                    Text = "Куртти (чина?)",
                    Difficulty = QuestionDifficulty.Hard,
                    Answers = new()
                    {
                        new AnswerSeedItem { Text = "Где?", IsCorrect = true },
                        new AnswerSeedItem { Text = "Куда?", IsCorrect = true },
                        new AnswerSeedItem { Text = "Зачем?", IsCorrect = false }
                    }
                },
                new QuestionSeedItem
                {
                    Text = "Гьанна",
                    Difficulty = QuestionDifficulty.Medium,
                    Answers = new()
                    {
                        new AnswerSeedItem { Text = "Потом", IsCorrect = false },
                        new AnswerSeedItem { Text = "Сейчас", IsCorrect = true },
                        new AnswerSeedItem { Text = "Вчера", IsCorrect = false }
                    }
                },
                new QuestionSeedItem
                {
                    Text = "Иш",
                    Difficulty = QuestionDifficulty.Hard,
                    Answers = new()
                    {
                        new AnswerSeedItem { Text = "Тот", IsCorrect = false },
                        new AnswerSeedItem { Text = "Этот", IsCorrect = true },
                        new AnswerSeedItem { Text = "Весь", IsCorrect = false }
                    }
                },
                new QuestionSeedItem
                {
                    Text = "Гье",
                    Difficulty = QuestionDifficulty.Easy,
                    Answers = new()
                    {
                        new AnswerSeedItem { Text = "Да", IsCorrect = true },
                        new AnswerSeedItem { Text = "Нет", IsCorrect = false },
                        new AnswerSeedItem { Text = "Может", IsCorrect = false }
                    }
                },
                new QuestionSeedItem
                {
                    Text = "Агьари",
                    Difficulty = QuestionDifficulty.Hard,
                    Answers = new()
                    {
                        new AnswerSeedItem { Text = "Иди", IsCorrect = false },
                        new AnswerSeedItem { Text = "Сюда", IsCorrect = false },
                        new AnswerSeedItem { Text = "Нет", IsCorrect = true }
                    }
                }
            }
        },
    };
}