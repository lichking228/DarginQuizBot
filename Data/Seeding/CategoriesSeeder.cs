using DargwaQuiz.Models;
using Microsoft.EntityFrameworkCore;

namespace DargwaQuiz.Data.Seeding;

public static class CategoriesSeeder
{
    public static void Seed(QuizDbContext context, List<CategorySeedItem> catalog)
    {
        foreach (var categorySeed in catalog)
        {
            var category = context.Categories.FirstOrDefault(c => c.Name == categorySeed.Name);
            if (category == null)
            {
                category = new Category
                {
                    Name = categorySeed.Name,
                    NameDargwa = categorySeed.NameDargwa,
                    Description = categorySeed.Description
                };
                context.Categories.Add(category);
                context.SaveChanges();
            }
            else
            {
                category.NameDargwa = categorySeed.NameDargwa;
                category.Description = categorySeed.Description;
                context.SaveChanges();
            }

            foreach (var questionSeed in categorySeed.Questions)
            {
                var question = context.Questions
                    .Include(q => q.Answers)
                    .FirstOrDefault(q => q.CategoryId == category.Id && q.Text == questionSeed.Text);

                if (question == null)
                {
                    question = new Question
                    {
                        CategoryId = category.Id,
                        Text = questionSeed.Text,
                        Difficulty = questionSeed.Difficulty,
                        IsActive = true,
                        Answers = questionSeed.Answers.Select(a => new Answer
                        {
                            Text = a.Text,
                            IsCorrect = a.IsCorrect
                        }).ToList()
                    };

                    context.Questions.Add(question);
                    context.SaveChanges();
                    continue;
                }

                question.Difficulty = questionSeed.Difficulty;
                question.IsActive = true;

                context.Answers.RemoveRange(question.Answers);
                question.Answers = questionSeed.Answers.Select(a => new Answer
                {
                    Text = a.Text,
                    IsCorrect = a.IsCorrect
                }).ToList();

                context.SaveChanges();
            }

            var actualTexts = categorySeed.Questions
                .Select(q => q.Text)
                .ToHashSet();

            var staleQuestions = context.Questions
                .Where(q => q.CategoryId == category.Id && !actualTexts.Contains(q.Text) && q.IsActive)
                .ToList();

            if (staleQuestions.Count == 0)
            {
                continue;
            }

            foreach (var staleQuestion in staleQuestions)
            {
                staleQuestion.IsActive = false;
            }

            context.SaveChanges();
        }
    }
}