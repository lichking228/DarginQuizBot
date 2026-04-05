using DargwaQuiz.Data;
using DargwaQuiz.Handlers;
using DargwaQuiz.Services.Implementations;
using DargwaQuiz.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddDbContext<QuizDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<ITelegramBotClient>(_ =>
{
    var token = builder.Configuration["TelegramBot:Token"];
    if (string.IsNullOrWhiteSpace(token))
        throw new InvalidOperationException("TelegramBot:Token is missing in configuration.");

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
var logger = app.Services.GetRequiredService<ILoggerFactory>().CreateLogger("TelegramBot");

// DB init
using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<QuizDbContext>();
        context.Database.Migrate();
        DbInitializer.Initialize(context);
        logger.LogInformation("Database initialized and seeded successfully.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error initializing database.");
    }
}

var botClient = app.Services.GetRequiredService<ITelegramBotClient>();
using var cts = new CancellationTokenSource();
app.Lifetime.ApplicationStopping.Register(() => cts.Cancel());

var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = Array.Empty<UpdateType>()
};

// Startup check
try
{
    var me = await botClient.GetMeAsync(cts.Token);
    logger.LogInformation("Telegram bot authorized as @{Username} (Id: {BotId}).", me.Username, me.Id);
}
catch (ApiRequestException ex)
{
    // usually invalid token / unauthorized
    logger.LogCritical(ex, "Telegram authorization failed. ErrorCode: {ErrorCode}. Check TelegramBot:Token.", ex.ErrorCode);
    throw;
}
catch (RequestException ex) when (ex.InnerException is HttpRequestException)
{
    // network/VPN/proxy/firewall issue: don't crash, keep app alive
    logger.LogError(ex, "Telegram API is unreachable now (timeout/DNS/proxy/firewall). App will continue and polling will retry.");
}
catch (RequestException ex)
{
    logger.LogCritical(ex, "Unexpected Telegram request error during startup check.");
    throw;
}

// Polling
botClient.StartReceiving(
    updateHandler: async (ITelegramBotClient _, Update update, CancellationToken cancellationToken) =>
    {
        try
        {
            using var scope = app.Services.CreateScope();
            var botService = scope.ServiceProvider.GetRequiredService<ITelegramBotService>();
            await botService.HandleUpdateAsync(update);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            logger.LogInformation("Update handling cancelled.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled error inside update handler.");
        }
    },
    pollingErrorHandler: (ITelegramBotClient _, Exception ex, CancellationToken _) =>
    {
        if (ex is ApiRequestException apiEx)
        {
            logger.LogError(apiEx, "Telegram API polling error. Code: {ErrorCode}, Message: {Message}", apiEx.ErrorCode, apiEx.Message);
        }
        else if (ex is HttpRequestException httpEx)
        {
            logger.LogError(httpEx, "HTTP/network polling error while calling Telegram API.");
        }
        else
        {
            logger.LogError(ex, "Unexpected polling error from Telegram bot client.");
        }

        return Task.CompletedTask;
    },
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

logger.LogInformation("Bot is up and running via polling. Press Ctrl+C to stop.");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();