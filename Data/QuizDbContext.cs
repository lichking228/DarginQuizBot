using Microsoft.EntityFrameworkCore;
using DargwaQuiz.Models;

namespace DargwaQuiz.Data;

public class QuizDbContext : DbContext
{
    public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options)
    {
    }

    // DbSet'ы для всех таблиц
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<QuizSession> QuizSessions { get; set; }
    public DbSet<UserAnswer> UserAnswers { get; set; }
    public DbSet<Achievement> Achievements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Конфиг User
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.TelegramId).IsUnique();
            entity.Property(e => e.Username).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.RegisteredAt).HasDefaultValueSql("NOW()");
        });

        // Конфиг Category
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.NameDargwa).HasMaxLength(200);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("NOW()");
        });

        // Конфиг Question
        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Text).HasMaxLength(500).IsRequired();
            entity.Property(e => e.TextDargwa).HasMaxLength(500);
            entity.Property(e => e.Explanation).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("NOW()");
            
            // Связь с Category
            entity.HasOne(e => e.Category)
                .WithMany(c => c.Questions)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Конфиг Answer
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Text).HasMaxLength(300).IsRequired();
            entity.Property(e => e.TextDargwa).HasMaxLength(300);
            
            // Связь с Question
            entity.HasOne(e => e.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(e => e.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Конфиг QuizSession
        modelBuilder.Entity<QuizSession>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.StartedAt).HasDefaultValueSql("NOW()");
            
            // Связь с User
            entity.HasOne(e => e.User)
                .WithMany(u => u.QuizSessions)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Связь с Category 
            entity.HasOne(e => e.Category)
                .WithMany()
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Конфиг UserAnswer
        modelBuilder.Entity<UserAnswer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.AnsweredAt).HasDefaultValueSql("NOW()");
            
            // Связь с User
            entity.HasOne(e => e.User)
                .WithMany(u => u.UserAnswers)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Связь с Question
            entity.HasOne(e => e.Question)
                .WithMany(q => q.UserAnswers)
                .HasForeignKey(e => e.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Связь с Answer
            entity.HasOne(e => e.Answer)
                .WithMany(a => a.UserAnswers)
                .HasForeignKey(e => e.AnswerId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Связь с QuizSession 
            entity.HasOne(e => e.QuizSession)
                .WithMany(qs => qs.UserAnswers)
                .HasForeignKey(e => e.QuizSessionId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Конфигу Achievement
        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Icon).HasMaxLength(10);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("NOW()");
            
            // Many-to-many связь с User
            entity.HasMany(e => e.Users)
                .WithMany(u => u.Achievements)
                .UsingEntity(j => j.ToTable("UserAchievements"));
        });
    }
}
