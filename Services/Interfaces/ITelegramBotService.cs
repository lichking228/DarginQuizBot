using Telegram.Bot.Types;

namespace DargwaQuiz.Services.Interfaces;

public interface ITelegramBotService
{
    Task HandleUpdateAsync(Update update);
    Task SendMessageAsync(long chatId, string text);
    Task SetWebhookAsync(string webhookUrl);
}