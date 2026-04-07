using DargwaQuiz.Models;

namespace DargwaQuiz.Data.Seeding;

public static class AchievementsSeeder
{
    public static void Seed(QuizDbContext context)
    {
        var templates = new List<Achievement>
        {
            new() { Name = "Первый шаг", Description = "Набери 2500 очков", Icon = "🥉", RequiredScore = 2500 },
            new() { Name = "Знаток", Description = "Набери 7500 очков", Icon = "🥈", RequiredScore = 7500 },
            new() { Name = "Мастер викторин", Description = "Набери 15000 очков", Icon = "🥇", RequiredScore = 15000 },
            new() { Name = "Легенда", Description = "Набери 25000 очков", Icon = "🏆", RequiredScore = 25000 }
        };

        foreach (var template in templates)
        {
            var existing = context.Achievements.FirstOrDefault(a => a.Name == template.Name);
            if (existing == null)
            {
                context.Achievements.Add(template);
                continue;
            }

            existing.Description = template.Description;
            existing.Icon = template.Icon;
            existing.RequiredScore = template.RequiredScore;
        }

        context.SaveChanges();
    }
}