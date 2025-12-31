using DargwaQuiz.Enums;
using DargwaQuiz.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace DargwaQuiz.Handlers;

public class CommandHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUserService _userService;
    private readonly IStatisticsService _statisticsService;
    private readonly ILocalizationService _loc;
    private readonly QuizHandler _quizHandler;
    private readonly ILogger<CommandHandler> _logger;

    public CommandHandler(
        ITelegramBotClient botClient,
        IUserService userService,
        IStatisticsService statisticsService,
        ILocalizationService loc,
        QuizHandler quizHandler,
        ILogger<CommandHandler> logger)
    {
        _botClient = botClient;
        _userService = userService;
        _statisticsService = statisticsService;
        _loc = loc;
        _quizHandler = quizHandler;
        _logger = logger;
    }

    public async Task HandleCommandAsync(Message message)
    {
        if (message.From == null || message.Text == null) return;

        var telegramId = message.From.Id;
        var chatId = message.Chat.Id;

        // Обновляем активность
        await _userService.UpdateLastActiveAsync(telegramId);
        
        // Получаем текущий язык пользователя
        var language = await _userService.GetPreferredLanguageAsync(telegramId);

        var commandText = message.Text.Split(' ')[0].ToLower();

        switch (commandText)
        {
            case "/start":
                await HandleStartCommandAsync(message, language);
                break;
            case "/help":
                await HandleHelpCommandAsync(chatId, language);
                break;
            case "/quiz":
                await HandleQuizCommandAsync(message, language);
                break;
            case "/stats":
                await HandleStatsCommandAsync(message, language);
                break;
            case "/leaderboard":
                await HandleLeaderboardCommandAsync(chatId, language);
                break;
            case "/language":
                await HandleLanguageCommandAsync(chatId, language);
                break;
            case "/cancel":
                await HandleCancelCommandAsync(message, language);
                break;
            default:
                await HandleUnknownCommandAsync(chatId, language);
                break;
        }
    }

    private async Task HandleStartCommandAsync(Message message, UserLanguage lang)
    {
        if (message.From == null) return;

        var telegramId = message.From.Id;
        var chatId = message.Chat.Id;

        var user = await _userService.GetUserByTelegramIdAsync(telegramId);
        if (user == null)
        {
            await _userService.CreateUserAsync(
                telegramId,
                message.From.Username ?? "Unknown",
                message.From.FirstName,
                message.From.LastName
            );
            
            // Приветствие нового пользователя
            var text = string.Format(_loc.GetMessage("welcome_new", lang), message.From.FirstName);
            await _botClient.SendTextMessageAsync(chatId, text);
        }
        else
        {
            // Приветствие старого пользователя
            var text = string.Format(_loc.GetMessage("welcome_back", lang), message.From.FirstName);
            await _botClient.SendTextMessageAsync(chatId, text);
        }
    }

    private async Task HandleHelpCommandAsync(long chatId, UserLanguage lang)
    {
        var text = _loc.GetMessage("help_text", lang);
        await _botClient.SendTextMessageAsync(chatId, text, parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
    }

    private async Task HandleQuizCommandAsync(Message message, UserLanguage lang)
    {
        if (message.From == null) return;
        await _quizHandler.StartQuizAsync(message.From.Id, message.Chat.Id, lang);
    }

    private async Task HandleStatsCommandAsync(Message message, UserLanguage lang)
    {
        if (message.From == null) return;

        var stats = await _statisticsService.GetUserStatisticsAsync(message.From.Id);
        
        // Сборка сообщения статистики из локализованных частей
        var text = $"{_loc.GetMessage("stats_header", lang)}\n\n" +
                   $"{string.Format(_loc.GetMessage("stats_username", lang), stats.Username)}\n" +
                   $"{string.Format(_loc.GetMessage("stats_total_score", lang), stats.TotalScore)}\n" +
                   $"{string.Format(_loc.GetMessage("stats_quizzes", lang), stats.QuizzesCompleted)}\n" +
                   $"{string.Format(_loc.GetMessage("stats_average", lang), stats.AverageScore)}\n" +
                   $"{string.Format(_loc.GetMessage("stats_total_answers", lang), stats.TotalQuestionsAnswered)}\n" +
                   $"{string.Format(_loc.GetMessage("stats_correct", lang), stats.CorrectAnswers)}\n" +
                   $"{string.Format(_loc.GetMessage("stats_accuracy", lang), stats.AccuracyPercentage)}";

        await _botClient.SendTextMessageAsync(message.Chat.Id, text, parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
    }

    private async Task HandleLeaderboardCommandAsync(long chatId, UserLanguage lang)
    {
        var leaderboard = await _statisticsService.GetLeaderboardAsync(10);

        if (leaderboard.Count == 0)
        {
            await _botClient.SendTextMessageAsync(chatId, _loc.GetMessage("leaderboard_empty", lang));
            return;
        }

        var text = $"{_loc.GetMessage("leaderboard_title", lang)}\n\n";
        
        for (int i = 0; i < leaderboard.Count; i++)
        {
            var user = leaderboard[i];
            var medal = i switch { 0 => "🥇", 1 => "🥈", 2 => "🥉", _ => $"{i + 1}." };
            
            text += $"{medal} *{user.Username}* - {user.TotalScore}\n";
        }

        await _botClient.SendTextMessageAsync(chatId, text, parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
    }

    private async Task HandleLanguageCommandAsync(long chatId, UserLanguage lang)
    {
        var keyboard = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(_loc.GetMessage("language_russian", lang), "lang_ru"),
                InlineKeyboardButton.WithCallbackData(_loc.GetMessage("language_dargwa", lang), "lang_drg")
            }
        });

        await _botClient.SendTextMessageAsync(
            chatId,
            _loc.GetMessage("select_language", lang),
            replyMarkup: keyboard
        );
    }

    private async Task HandleCancelCommandAsync(Message message, UserLanguage lang)
    {
        if (message.From == null) return;
        await _quizHandler.CancelQuizAsync(message.From.Id, message.Chat.Id, lang);
    }

    private async Task HandleUnknownCommandAsync(long chatId, UserLanguage lang)
    {
        await _botClient.SendTextMessageAsync(chatId, _loc.GetMessage("error_unknown_command", lang));
    }
}
