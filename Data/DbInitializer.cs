using DargwaQuiz.Data.Seeding;

namespace DargwaQuiz.Data;

public static class DbInitializer
{
    public static void Initialize(QuizDbContext context)
    {
        context.Database.EnsureCreated();

        AchievementsSeeder.Seed(context);

        var catalog = CategoriesSeedCatalog.Build();
        CategoriesSeeder.Seed(context, catalog);
    }
}