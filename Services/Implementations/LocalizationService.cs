using DargwaQuiz.Enums;
using DargwaQuiz.Services.Interfaces;

namespace DargwaQuiz.Services.Implementations;

public class LocalizationService : ILocalizationService
{
    private readonly Dictionary<string, Dictionary<UserLanguage, string>> _messages = new()
    {
        // Приветствия
        ["welcome_new"] = new()
        {
            [UserLanguage.Russian] = "👋 Привет, {0}!\n\nДобро пожаловать в DargwaQuiz - бот для изучения даргинского языка!\n\nИспользуйте /help чтобы увидеть доступные команды.",
            [UserLanguage.Dargwa] = "Салам, {0}!\n\nХашкелди  DargwaQuiz-личи — дарган мез руркъес багьандан бот!\n\nПайдалабарая /help, лерти командаби чеэс багьандан."
        },
        ["welcome_back"] = new()
        {
            [UserLanguage.Russian] = "С возвращением, {0}! 🎉\n\nГотовы продолжить обучение? Используйте /quiz для начала викторины.",
            [UserLanguage.Dargwa] = "Зизи, {0}! 🎉\n\nРуркъни даимбирес хIядурлирив? Пайдалабарая /quiz викторина бехIбихьес багьандан."
        },
        
        // Помощь
        ["help_text"] = new()
        {
            [UserLanguage.Russian] = "📚 *Доступные команды:*\n\n" +
                                    "/start - Начать работу с ботом\n" +
                                    "/quiz - Начать викторину\n" +
                                    "/stats - Ваша статистика\n" +
                                    "/leaderboard - Таблица лидеров\n" +
                                    "/language - Изменить язык\n" +
                                    "/cancel - Отменить текущую викторину\n" +
                                    "/help - Показать эту справку\n\n" +
                                    "Выберите категорию и начните изучать даргинский язык! 🎯",
            [UserLanguage.Dargwa] = "📚 *Лерти командаби:*\n\n" +
                                   "/start - Ботличил хIянчи бехIбихьес\n" +
                                   "/quiz - Викторина бехIбихьес\n" +
                                   "/stats - ХIушала статистика\n" +
                                   "/leaderboard - Гьабкьябала сияхI\n" +
                                   "/language - Мез барсдарес\n" +
                                   "/cancel - Викторина уббяхъес\n" +
                                   "/help - Иш справка чебаахъес\n\n" +
                                   "Гьагъниси категория чеббикIая ва дарган мез руркъес бехIбихьяя! 🎯"
        },
        
        // Статистика
        ["stats_header"] = new()
        {
            [UserLanguage.Russian] = "📊 *Ваша статистика:*",
            [UserLanguage.Dargwa] = "📊 *ХIушала статистика:*"
        },
        ["stats_username"] = new()
        {
            [UserLanguage.Russian] = "👤 Пользователь: {0}",
            [UserLanguage.Dargwa] = "👤 Пайдалабиран: {0}"
        },
        ["stats_total_score"] = new()
        {
            [UserLanguage.Russian] = "🏆 Общий счет: {0}",
            [UserLanguage.Dargwa] = "🏆 АрагIебси кьадар: {0}"
        },
        ["stats_quizzes"] = new()
        {
            [UserLanguage.Russian] = "✅ Завершено викторин: {0}",
            [UserLanguage.Dargwa] = "✅ Викторина таманси саби: {0}"
        },
        ["stats_average"] = new()
        {
            [UserLanguage.Russian] = "📈 Средний счет: {0:F2}",
            [UserLanguage.Dargwa] = "📈 Урга кьадар: {0:F2}"
        },
        ["stats_total_answers"] = new()
        {
            [UserLanguage.Russian] = "❓ Всего ответов: {0}",
            [UserLanguage.Dargwa] = "❓ Лерилра жавабти кьадар: {0}"
        },
        ["stats_correct"] = new()
        {
            [UserLanguage.Russian] = "✔️ Правильных ответов: {0}",
            [UserLanguage.Dargwa] = "✔️ Хьулра жаваб: {0}"
        },
        ["stats_accuracy"] = new()
        {
            [UserLanguage.Russian] = "🎯 Точность: {0}%",
            [UserLanguage.Dargwa] = "🎯 Дурусдеш: {0}%"
        },
        
        // Викторина
        ["select_category"] = new()
        {
            [UserLanguage.Russian] = "📚 Выберите категорию для викторины:",
            [UserLanguage.Dargwa] = "📚 ГIягIниси категория чеббикIая викторина багьандан:"
        },
        ["all_categories"] = new()
        {
            [UserLanguage.Russian] = "🎲 Все категории",
            [UserLanguage.Dargwa] = "🎲 Лебилра категораби"
        },
        ["question"] = new()
        {
            [UserLanguage.Russian] = "❓ *Вопрос:*",
            [UserLanguage.Dargwa] = "❓ *Суал:*"
        },
        ["quiz_completed"] = new()
        {
            [UserLanguage.Russian] = "🎉 *Викторина завершена!*",
            [UserLanguage.Dargwa] = "🎉 *Викторина таманси саби!*"
        },
        ["results"] = new()
        {
            [UserLanguage.Russian] = "📊 Результаты:",
            [UserLanguage.Dargwa] = "📊 ХIясил:"
        },
        ["correct_answers"] = new()
        {
            [UserLanguage.Russian] = "✅ Правильных ответов: {0}/{1}",
            [UserLanguage.Dargwa] = "✅ Бархьси жавабти кьадар: {0}/{1}"
        },
        ["accuracy"] = new()
        {
            [UserLanguage.Russian] = "🎯 Точность: {0}%",
            [UserLanguage.Dargwa] = "🎯 Дурусдеш: {0}%"
        },
        ["score"] = new()
        {
            [UserLanguage.Russian] = "🏆 Набрано очков: {0}",
            [UserLanguage.Dargwa] = "🏆 Очкоти кьадар: {0}"
        },
        ["time"] = new()
        {
            [UserLanguage.Russian] = "⏱ Время: {0}",
            [UserLanguage.Dargwa] = "⏱ Замана: {0}"
        },
        ["try_again"] = new()
        {
            [UserLanguage.Russian] = "Хотите попробовать еще раз? /quiz",
            [UserLanguage.Dargwa] = "ГIурра ахтардибарес дигахъулрив? /quiz"
        },
        
        // Таблица лидеров
        ["leaderboard_title"] = new()
        {
            [UserLanguage.Russian] = "🏆 *Топ-10 игроков:*",
            [UserLanguage.Dargwa] = "🏆 *10 бяркьала ахӏти:*"
        },
        ["leaderboard_empty"] = new()
        {
            [UserLanguage.Russian] = "🏆 Таблица лидеров пока пуста. Будьте первым!",
            [UserLanguage.Dargwa] = "🏆 Лидерталис таблица къаси хӀялра. Ца-саби бихье!"
        },
        
        // Язык
        ["select_language"] = new()
        {
            [UserLanguage.Russian] = "🌐 Выберите язык / Мез чеббикIая:",
            [UserLanguage.Dargwa] = "🌐 Мез чеббикIая / Выберите язык:"
        },
        ["language_russian"] = new()
        {
            [UserLanguage.Russian] = "Русский",
            [UserLanguage.Dargwa] = "Урус мез"
        },
        ["language_dargwa"] = new()
        {
            [UserLanguage.Russian] = "Даргинский",
            [UserLanguage.Dargwa] = "Дарган мез"
        },
        ["language_changed_russian"] = new()
        {
            [UserLanguage.Russian] = "✅ Язык изменен на русский",
            [UserLanguage.Dargwa] = "✅ Мез урус мезличи барсбиуб"
        },
        ["language_changed_dargwa"] = new()
        {
            [UserLanguage.Russian] = "✅ Язык изменен на даргинский",
            [UserLanguage.Dargwa] = "✅ Мез дарган мезличи барсбиуб"
        },

        // Ошибки
        ["error_not_registered"] = new()
        {
            [UserLanguage.Russian] = "❌ Сначала используйте команду /start для регистрации.",
            [UserLanguage.Dargwa] = "❌ Регистрация баркьес багьандан, гьала-гьала /start бикӀул чебейгӀ пайдалабаркьа"
        },
        ["error_active_quiz"] = new()
        {
            [UserLanguage.Russian] = "⚠️ У вас уже есть активная викторина. Используйте /cancel чтобы отменить её.",
            [UserLanguage.Dargwa] = "⚠️ХӀела ачамлис бузул викторина леб. Ил гӀекӀес багьандан /cancel пайдалабаркьа"
        },
        ["error_no_active_quiz"] = new()
        {
            [UserLanguage.Russian] = "❌ У вас нет активной викторины.",
            [UserLanguage.Dargwa] = "❌ ХӀела бузул викторина агара."
        },
        ["error_unknown_command"] = new()
        {
            [UserLanguage.Russian] = "❓ Неизвестная команда. Используйте /help для просмотра доступных команд.",
            [UserLanguage.Dargwa] = "❓ БалхӀебалул чебейгӀ саби. ДикӀес вирути чебейгӀуни чедаэс багьандан /help бузахъен"
        },
        ["quiz_cancelled"] = new()
        {
            [UserLanguage.Russian] = "✅ Викторина отменена. Начните новую с помощью /quiz",
            [UserLanguage.Dargwa] = "✅ Викторина уббяхъили саби. Сагал баркьес багьандан /quiz\" бузахъен,"
        },
        
        // Правильно/Неправильно
        ["answer_correct"] = new()
        {
            [UserLanguage.Russian] = "✅ Правильно!",
            [UserLanguage.Dargwa] = "✅ Бархьси саби!!"
        },
        ["answer_wrong"] = new()
        {
            [UserLanguage.Russian] = "❌ Неправильно. Правильный ответ: {0}",
            [UserLanguage.Dargwa] = "❌ БалкIли саби. Бархьси жаваб:: {0}"
        }
    };

    public string GetMessage(string key, UserLanguage language)
    {
        if (_messages.TryGetValue(key, out var translations))
        {
            if (translations.TryGetValue(language, out var message))
            {
                return message;
            }
        }
        return $"[Missing translation: {key}]";
    }

    public string GetCommand(string command, UserLanguage language)
    {
        return GetMessage(command, language);
    }

    public string GetError(string errorKey, UserLanguage language)
    {
        return GetMessage(errorKey, language);
    }
}
