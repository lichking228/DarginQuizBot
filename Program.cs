using DargwaQuiz.Data;
using DargwaQuiz.Handlers;
using DargwaQuiz.Services.Implementations;
using DargwaQuiz.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Polling;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddDbContext<QuizDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<ITelegramBotClient>(provider =>
{
    var token = builder.Configuration["TelegramBot:Token"];
    if (string.IsNullOrEmpty(token)) throw new ArgumentNullException("TelegramBot:Token is missing!");
    return new TelegramBotClient(token);
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();
builder.Services.AddScoped<ILocalizationService, LocalizationService>();

builder.Services.AddScoped<QuizHandler>();
builder.Services.AddScoped<CommandHandler>();
builder.Services.AddScoped<CallbackQueryHandler>();

builder.Services.AddScoped<ITelegramBotService, TelegramBotService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<QuizDbContext>();
        context.Database.Migrate();
        DbInitializer.Initialize(context);
        Console.WriteLine("✅ Database initialized and seeded successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error initializing database: {ex.Message}");
    }
}

var botClient = app.Services.GetRequiredService<ITelegramBotClient>();

using var cts = new CancellationTokenSource();
var receiverOptions = new ReceiverOptions 
{ 
    AllowedUpdates = [] 
};

botClient.StartReceiving(
    updateHandler: async (bot, update, ct) =>
    {
        try
        {
            using var scope = app.Services.CreateScope();
            var botService = scope.ServiceProvider.GetRequiredService<ITelegramBotService>();
            
            await botService.HandleUpdateAsync(update);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inside update handler: {ex.Message}");
        }
    },
    pollingErrorHandler: async (bot, ex, ct) => 
    {
        Console.WriteLine($"Telegram API Error: {ex.Message}");
        await Task.CompletedTask;
    },
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

Console.WriteLine("🚀 Bot is up and running via POLLING! Press Ctrl+C to stop.");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
