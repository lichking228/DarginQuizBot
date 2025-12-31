using DargwaQuiz.DTOs;
using DargwaQuiz.Enums;
using DargwaQuiz.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace DargwaQuiz.Handlers;

public class QuizHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IQuizService _quizService;
    private readonly IUserService _userService;
    private readonly IStatisticsService _statisticsService;
    private readonly ILocalizationService _loc;
    private readonly ILogger<QuizHandler> _logger;

    public QuizHandler(
        ITelegramBotClient botClient,
        IQuizService quizService,
        IUserService userService,
        IStatisticsService statisticsService,
        ILocalizationService loc,
        ILogger<QuizHandler> logger)
    {
        _botClient = botClient;
        _quizService = quizService;
        _userService = userService;
        _statisticsService = statisticsService;
        _loc = loc;
        _logger = logger;
    }

    public async Task StartQuizAsync(long telegramId, long chatId, UserLanguage lang)
    {
        var user = await _userService.GetUserByTelegramIdAsync(telegramId);
        if (user == null)
        {
            await _botClient.SendTextMessageAsync(chatId, _loc.GetMessage("error_not_registered", lang));
            return;
        }

        var activeQuiz = await _quizService.GetActiveQuizSessionAsync(user.Id);
        if (activeQuiz != null)
        {
            await _botClient.SendTextMessageAsync(chatId, _loc.GetMessage("error_active_quiz", lang));
            return;
        }

        var categories = await _quizService.GetAllCategoriesAsync();
        
        var keyboardButtons = new List<IEnumerable<InlineKeyboardButton>>();
        
        foreach (var category in categories)
        {
            var catName = (lang == UserLanguage.Dargwa && !string.IsNullOrEmpty(category.NameDargwa)) 
                ? category.NameDargwa 
                : category.Name;

            keyboardButtons.Add(new[] 
            { 
                InlineKeyboardButton.WithCallbackData(catName, $"category_{category.Id}") 
            });
        }

        keyboardButtons.Add(new[] 
        { 
            InlineKeyboardButton.WithCallbackData(_loc.GetMessage("all_categories", lang), "category_all") 
        });

        var keyboard = new InlineKeyboardMarkup(keyboardButtons);

        await _botClient.SendTextMessageAsync(chatId,
            _loc.GetMessage("select_category", lang),
            replyMarkup: keyboard);
    }

    public async Task CreateQuizAndSendFirstQuestionAsync(long chatId, long telegramId, int? categoryId)
    {
        var user = await _userService.GetUserByTelegramIdAsync(telegramId);
        if (user == null) return;
        
        var lang = user.PreferredLanguage;

        var session = await _quizService.CreateQuizSessionAsync(user.Id, categoryId, 10);
        await SendNextQuestionAsync(session.Id, chatId, lang);
    }

    public async Task SendNextQuestionAsync(int quizSessionId, long chatId, UserLanguage lang)
    {
        var question = await _quizService.GetNextQuestionAsync(quizSessionId);

        if (question == null)
        {
            await CompleteQuizAsync(quizSessionId, chatId, lang);
            return;
        }

        var questionHeader = _loc.GetMessage("question", lang);
        
        var questionBody = (lang == UserLanguage.Dargwa && !string.IsNullOrEmpty(question.TextDargwa))
            ? question.TextDargwa
            : question.Text;

        var text = $"{questionHeader}\n\n{questionBody}";

        var answerButtons = new List<IEnumerable<InlineKeyboardButton>>();
        
        foreach (var answer in question.Answers)
        {
            var answerText = (lang == UserLanguage.Dargwa && !string.IsNullOrEmpty(answer.TextDargwa))
                ? answer.TextDargwa
                : answer.Text;

            answerButtons.Add(new[]
            {
                InlineKeyboardButton.WithCallbackData(
                    answerText, 
                    $"answer_{quizSessionId}_{question.Id}_{answer.Id}")
            });
        }

        var keyboard = new InlineKeyboardMarkup(answerButtons);

        await _botClient.SendTextMessageAsync(chatId, text,
            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
            replyMarkup: keyboard);
    }

    public async Task HandleAnswerAsync(long chatId, long telegramId, int sessionId, int questionId, int answerId)
    {
        int timeSpent = 15; 
        
        var isCorrect = await _quizService.SubmitAnswerAsync(sessionId, questionId, answerId, timeSpent);
        
        var user = await _userService.GetUserByTelegramIdAsync(telegramId);
        var lang = user?.PreferredLanguage ?? UserLanguage.Russian;

        if (isCorrect)
        {
            await _botClient.SendTextMessageAsync(chatId, _loc.GetMessage("answer_correct", lang));
        }
        else
        {
            var correctText = await _quizService.GetCorrectAnswerTextAsync(questionId);
            await _botClient.SendTextMessageAsync(chatId, 
                string.Format(_loc.GetMessage("answer_wrong", lang), correctText));
        }

        await Task.Delay(1000);
        await SendNextQuestionAsync(sessionId, chatId, lang);
    }

    private async Task CompleteQuizAsync(int quizSessionId, long chatId, UserLanguage lang)
    {
        var session = await _quizService.CompleteQuizSessionAsync(quizSessionId);
        if (session == null) return;

        var result = await _statisticsService.GetQuizResultAsync(quizSessionId);

        var resultText = $"{_loc.GetMessage("quiz_completed", lang)}\n\n" +
                        $"{_loc.GetMessage("results", lang)}\n" +
                        $"{string.Format(_loc.GetMessage("correct_answers", lang), result.CorrectAnswers, result.TotalQuestions)}\n" +
                        $"{string.Format(_loc.GetMessage("accuracy", lang), result.AccuracyPercentage)}\n" +
                        $"{string.Format(_loc.GetMessage("score", lang), result.Score)}\n" +
                        $"{string.Format(_loc.GetMessage("time", lang), result.Duration.ToString("mm\\:ss"))}\n\n" +
                        $"{_loc.GetMessage("try_again", lang)}";

        await _botClient.SendTextMessageAsync(chatId, resultText,
            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
    }

    public async Task CancelQuizAsync(long telegramId, long chatId, UserLanguage lang)
    {
        var user = await _userService.GetUserByTelegramIdAsync(telegramId);
        if (user == null) return;

        var activeQuiz = await _quizService.GetActiveQuizSessionAsync(user.Id);
        if (activeQuiz == null)
        {
            await _botClient.SendTextMessageAsync(chatId, _loc.GetMessage("error_no_active_quiz", lang));
            return;
        }

        await _quizService.CompleteQuizSessionAsync(activeQuiz.Id);
        await _botClient.SendTextMessageAsync(chatId, _loc.GetMessage("quiz_cancelled", lang));
    }
}
