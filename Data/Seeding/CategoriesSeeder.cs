using DargwaQuiz.Models;
using Microsoft.EntityFrameworkCore;

namespace DargwaQuiz.Data.Seeding;

public static class CategoriesSeeder
{
    public static void Seed(QuizDbContext context, List<CategorySeedItem> catalog)
    {
        foreach (var categorySeed in catalog)
        {
            var category = context.Categories
                .FirstOrDefault(c => c.Name == categorySeed.Name);

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
                        IsActive = true
                    };

                    context.Questions.Add(question);
                    context.SaveChanges();
                }
                else
                {
                    question.Difficulty = questionSeed.Difficulty;
                    question.IsActive = true;
                    context.SaveChanges();
                }

                SyncAnswersInPlace(context, question, questionSeed.Answers);
            }
        }
    }

    private static void SyncAnswersInPlace(
        QuizDbContext context,
        Question question,
        List<AnswerSeedItem> seedAnswers)
    {
        var existingAnswers = context.Answers
            .Where(a => a.QuestionId == question.Id)
            .OrderBy(a => a.OrderIndex)
            .ToList();

        for (int i = 0; i < seedAnswers.Count; i++)
        {
            var seed = seedAnswers[i];

            if (i < existingAnswers.Count)
            {
                var current = existingAnswers[i];
                current.Text = seed.Text;
                current.IsCorrect = seed.IsCorrect;
                current.OrderIndex = i;
            }
            else
            {
                context.Answers.Add(new Answer
                {
                    QuestionId = question.Id,
                    Text = seed.Text,
                    IsCorrect = seed.IsCorrect,
                    OrderIndex = i
                });
            }
        }
        
        context.SaveChanges();
    }
}