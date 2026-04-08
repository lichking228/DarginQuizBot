using DargwaQuiz.Enums;
using DargwaQuiz.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DargwaQuiz.Handlers;

public class CallbackQueryHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUserService _userService;
    private readonly ILocalizationService _loc;
    private readonly QuizHandler _quizHandler;

    public CallbackQueryHandler(
        ITelegramBotClient botClient,
        IUserService userService,
        ILocalizationService loc,
        QuizHandler quizHandler,
        ILogger<CallbackQueryHandler> logger)
    {
        _botClient = botClient;
        _userService = userService;
        _loc = loc;
        _quizHandler = quizHandler;
    }

    public async Task HandleCallbackAsync(CallbackQuery callbackQuery)
    {
        var data = callbackQuery.Data;
        var telegramId = callbackQuery.From.Id;
        var message = callbackQuery.Message;
        var chatId = message?.Chat.Id;

        if (data == null || chatId == null)
            return;

        if (data == "lang_ru" || data == "lang_drg")
        {
            var newLang = data == "lang_ru" ? UserLanguage.Russian : UserLanguage.Dargwa;

            await _userService.SetPreferredLanguageAsync(telegramId, newLang);

            var text = newLang == UserLanguage.Russian
                ? _loc.GetMessage("language_changed_russian", newLang)
                : _loc.GetMessage("language_changed_dargwa", newLang);

            await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id, text);
            await _botClient.SendTextMessageAsync(chatId.Value, text);
            return;
        }

        if (data.StartsWith("category_"))
        {
            var categoryPart = data.Replace("category_", "");
            int? categoryId = categoryPart == "all" ? null : int.Parse(categoryPart);

            await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
            await _quizHandler.CreateQuizAndSendFirstQuestionAsync(chatId.Value, telegramId, categoryId);
            return;
        }

        if (data.StartsWith("answer_"))
        {
            if (message == null)
                return;

            var parts = data.Split('_');
            if (parts.Length != 4)
                return;

            var sessionId = int.Parse(parts[1]);
            var questionId = int.Parse(parts[2]);
            var answerId = int.Parse(parts[3]);

            await _quizHandler.HandleAnswerAsync(
                chatId.Value,
                telegramId,
                sessionId,
                questionId,
                answerId,
                message.MessageId);

            await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
        }
    }
}