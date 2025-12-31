using DargwaQuiz.Handlers;
using DargwaQuiz.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.Logging;

namespace DargwaQuiz.Services.Implementations;

public class TelegramBotService : ITelegramBotService
{
    private readonly ITelegramBotClient _botClient;
    private readonly CommandHandler _commandHandler;
    private readonly CallbackQueryHandler _callbackHandler;
    private readonly ILogger<TelegramBotService> _logger;

    public TelegramBotService(
        ITelegramBotClient botClient,
        CommandHandler commandHandler,
        CallbackQueryHandler callbackHandler,
        ILogger<TelegramBotService> logger)
    {
        _botClient = botClient;
        _commandHandler = commandHandler;
        _callbackHandler = callbackHandler;
        _logger = logger;
    }

    public async Task HandleUpdateAsync(Update update)
    {
        _logger.LogInformation("Обработка update: {Type}", update.Type);

        try 
        {
            if (update.Type == UpdateType.Message && update.Message?.Text != null)
            {
                await _commandHandler.HandleCommandAsync(update.Message);
            }
            else if (update.Type == UpdateType.CallbackQuery && update.CallbackQuery != null)
            {
                await _callbackHandler.HandleCallbackAsync(update.CallbackQuery);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обработке update");
        }
    }

    public async Task SendMessageAsync(long chatId, string text)
    {
        await _botClient.SendTextMessageAsync(chatId, text);
    }

    public async Task SetWebhookAsync(string webhookUrl)
    {
        await _botClient.SetWebhookAsync(webhookUrl);
    }
}
