using DargwaQuiz.Enums;

namespace DargwaQuiz.Data.Seeding;

public static class CategoriesSeedCatalog
{
    public static List<CategorySeedItem> Build() => new()
    {
        new CategorySeedItem
        {
            Name = "Основы",
            NameDargwa = "БехIбихьуд",
            Description = "Базовые слова",
            Questions = new()
            {
                CreateQuestion("Салам", QuestionDifficulty.Easy, ("Привет", true), ("Пока", false), ("Идти", false)),
                CreateQuestion("ХIу", QuestionDifficulty.Easy, ("Я", false), ("Ты", true), ("Мы", false)),
                CreateQuestion("Ну", QuestionDifficulty.Easy, ("Он", false), ("Я", true), ("Они", false)),
                CreateQuestion("Нуша", QuestionDifficulty.Medium, ("Его", false), ("Их", false), ("Мы", true)),
                CreateQuestion("Чи?", QuestionDifficulty.Medium, ("Где?", false), ("Кто?", true), ("Когда?", false)),
                CreateQuestion("Куртти (чина?)", QuestionDifficulty.Hard, ("Где?", true), ("Куда?", true), ("Зачем?", false)),
                CreateQuestion("Гьанна", QuestionDifficulty.Medium, ("Потом", false), ("Сейчас", true), ("Вчера", false)),
                CreateQuestion("Иш", QuestionDifficulty.Hard, ("Тот", false), ("Этот", true), ("Весь", false)),
                CreateQuestion("Гье", QuestionDifficulty.Easy, ("Да", true), ("Нет", false), ("Может", false)),
                CreateQuestion("Агьари", QuestionDifficulty.Hard, ("Иди", false), ("Сюда", false), ("Нет", true))
            }
        },
        new CategorySeedItem
        {
            Name = "Животные и насекомые",
            NameDargwa = "ХIеванаш",
            Description = "Названия животных и насекомых",
            Questions = new()
            {
                CreateQuestion("БецI", QuestionDifficulty.Easy, ("Собака", false), ("Волк", true), ("Кошка", false)),
                CreateQuestion("ГIиргIа", QuestionDifficulty.Hard, ("Птица", false), ("Летучая мышь", true), ("Бабочка", false)),
                CreateQuestion("Хя", QuestionDifficulty.Easy, ("Кошка", false), ("Собака", true), ("Корова", false)),
                CreateQuestion("Кквяртт", QuestionDifficulty.Hard, ("Курица", false), ("Наседка", true), ("Петух", false)),
                CreateQuestion("ЧитIа", QuestionDifficulty.Easy, ("Птица", false), ("Кошка", true), ("Мышь", false)),
                CreateQuestion("ЦIика", QuestionDifficulty.Hard, ("Муха", false), ("Блоха", true), ("Муравей", false)),
                CreateQuestion("ГIяра", QuestionDifficulty.Medium, ("Заяц", true), ("Белка", false), ("Мышь", false)),
                CreateQuestion("Гамуш", QuestionDifficulty.Hard, ("Олень", false), ("Буйвол", true), ("Медведь", false)),
                CreateQuestion("ВацIараци", QuestionDifficulty.Easy, ("Медведь", true), ("Волк", false), ("Лиса", false)),
                CreateQuestion("КьикI", QuestionDifficulty.Hard, ("Древесный червяк", true), ("Паук", false), ("Кузнечик", false))
            }
        },
        new CategorySeedItem
        {
            Name = "Еда и посуда",
            NameDargwa = "Беркани ва кьялти",
            Description = "Еда, напитки и кухонная утварь",
            Questions = new()
            {
                CreateQuestion("Шин", QuestionDifficulty.Easy, ("Чай", false), ("Сок", false), ("Вода", true)),
                CreateQuestion("Ццихьин", QuestionDifficulty.Hard, ("Вид супа", false), ("Блюдо с холодным молоком, луком и сыром", true), ("Каша", false)),
                CreateQuestion("Ккац", QuestionDifficulty.Easy, ("Хлеб", true), ("Мясо", false), ("Каша", false)),
                CreateQuestion("Касир", QuestionDifficulty.Hard, ("Хлеб", false), ("Каша из пшеничной муки", true), ("Мясо", false)),
                CreateQuestion("Диъ", QuestionDifficulty.Medium, ("Рыба", false), ("Мясо", true), ("Птица", false)),
                CreateQuestion("ЦIанкари", QuestionDifficulty.Hard, ("Котел", false), ("Сковородка для жарки зерна", true), ("Ложка", false)),
                CreateQuestion("Ниъ", QuestionDifficulty.Medium, ("Масло", false), ("Молоко", true), ("Творог", false)),
                CreateQuestion("КьурчIи-нихI", QuestionDifficulty.Hard, ("Вино", false), ("Перебродившая молочная водка", true), ("Компот", false)),
                CreateQuestion("Чай", QuestionDifficulty.Easy, ("Суп", false), ("Чай", true), ("Компот", false)),
                CreateQuestion("Гаваж", QuestionDifficulty.Hard, ("Казан", true), ("Тарелка", false), ("Вилка", false))
            }
        },
        new CategorySeedItem
        {
            Name = "Одежда и текстиль",
            NameDargwa = "Палтар",
            Description = "Одежда и текстильные предметы",
            Questions = new()
            {
                CreateQuestion("КьяпIа", QuestionDifficulty.Easy, ("Шапка", true), ("Шарф", false), ("Перчатки", false)),
                CreateQuestion("Варгьи", QuestionDifficulty.Hard, ("Шапка", false), ("Бурка", true), ("Пояс", false)),
                CreateQuestion("Гор", QuestionDifficulty.Medium, ("Рубашка", true), ("Майка", false), ("Кофта", false)),
                CreateQuestion("КватIни", QuestionDifficulty.Hard, ("Носки", false), ("Рабочие перчатки из шкур", true), ("Шарф", false)),
                CreateQuestion("Юрт", QuestionDifficulty.Medium, ("Куртка", false), ("Пальто", true), ("Шуба", false)),
                CreateQuestion("КвялхIя", QuestionDifficulty.Hard, ("Клубок от мотка пряжи", true), ("Иголка", false), ("Нить", false)),
                CreateQuestion("Цулли", QuestionDifficulty.Hard, ("Сапоги", false), ("Носки", true), ("Туфли", false)),
                CreateQuestion("ЦIатти", QuestionDifficulty.Hard, ("Шелк", false), ("Слипшийся клок шерсти", true), ("Хлопок", false)),
                CreateQuestion("Кьяшпалтар", QuestionDifficulty.Medium, ("Обувь", true), ("Перчатки", false), ("Шляпа", false)),
                CreateQuestion("Валчагъ", QuestionDifficulty.Hard, ("Сапоги", false), ("Мужская верхняя одежда", true), ("Рубаха", false))
            }
        },
        new CategorySeedItem
        {
            Name = "Природа и ландшафт",
            NameDargwa = "ТIабигIят",
            Description = "Слова о природе и местности",
            Questions = new()
            {
                CreateQuestion("Дубура", QuestionDifficulty.Easy, ("Равнина", false), ("Гора", true), ("Холм", false)),
                CreateQuestion("Капарай", QuestionDifficulty.Hard, ("Лес", false), ("Большая равнина", true), ("Ущелье", false)),
                CreateQuestion("БерхIи", QuestionDifficulty.Easy, ("Луна", false), ("Солнце", true), ("Звезда", false)),
                CreateQuestion("Ццабхари", QuestionDifficulty.Hard, ("Гроза", false), ("Радуга", true), ("Туман", false)),
                CreateQuestion("ВацIа", QuestionDifficulty.Medium, ("Сад", false), ("Лес", true), ("Поле", false)),
                CreateQuestion("ЦIур", QuestionDifficulty.Hard, ("Мост", false), ("Сторожевая башня", true), ("Стена", false)),
                CreateQuestion("ЦIа", QuestionDifficulty.Easy, ("Вода", false), ("Огонь", true), ("Земля", false)),
                CreateQuestion("Кикьла", QuestionDifficulty.Hard, ("Озеро", false), ("Маленький родник", true), ("Водопад", false)),
                CreateQuestion("Марка", QuestionDifficulty.Medium, ("Снег", false), ("Град", false), ("Дождь", true)),
                CreateQuestion("Кьавкьав", QuestionDifficulty.Hard, ("Водоворот", true), ("Град", false), ("Ветер", false))
            }
        },
        new CategorySeedItem
        {
            Name = "Сельское хозяйство и инструменты",
            NameDargwa = "Шишла майишат",
            Description = "Термины, связанные с хозяйством",
            Questions = new()
            {
                CreateQuestion("Ккутан", QuestionDifficulty.Medium, ("Плуг", true), ("Лопата", false), ("Грабли", false)),
                CreateQuestion("Ццива", QuestionDifficulty.Hard, ("Молоток", false), ("Инструмент для снятия коры", true), ("Пила", false)),
                CreateQuestion("Муза", QuestionDifficulty.Medium, ("Телёнок", true), ("Ягнёнок", false), ("Жеребёнок", false)),
                CreateQuestion("Ккунатти", QuestionDifficulty.Hard, ("Шерстяные нити", false), ("Конопляные нити", true), ("Шелковые нити", false)),
                CreateQuestion("Гебен", QuestionDifficulty.Medium, ("Длинный стог сена", true), ("Мешок", false), ("Корзина", false)),
                CreateQuestion("Кинаурхаб", QuestionDifficulty.Hard, ("Верхний жернов мельницы", true), ("Топор", false), ("Сито", false)),
                CreateQuestion("Кьяркь", QuestionDifficulty.Medium, ("Трава", false), ("Остатки соломы после обмолота", true), ("Зерно", false)),
                CreateQuestion("ЦIуппи", QuestionDifficulty.Hard, ("Таган (треножник)", true), ("Нож", false), ("Вилка", false)),
                CreateQuestion("Курик", QuestionDifficulty.Medium, ("Дрова", false), ("Кизяк", true), ("Уголь", false)),
                CreateQuestion("Ккап", QuestionDifficulty.Medium, ("Лист", false), ("Корень", true), ("Стебель", false))
            }
        },
        new CategorySeedItem
        {
            Name = "Дом и постройки",
            NameDargwa = "Хъали ва гIяшлар",
            Description = "Слова о доме и строениях",
            Questions = new()
            {
                CreateQuestion("Юрт", QuestionDifficulty.Easy, ("Дом", true), ("Двор", false), ("Сад", false)),
                CreateQuestion("ГIибтухъ", QuestionDifficulty.Hard, ("Спальня", false), ("Кладовка", true), ("Крыша", false)),
                CreateQuestion("Унза", QuestionDifficulty.Medium, ("Окно", false), ("Дверь", true), ("Стена", false)),
                CreateQuestion("Кьулсутни", QuestionDifficulty.Hard, ("Кровать", false), ("Шкаф для посуды", true), ("Стол", false)),
                CreateQuestion("Гьуни", QuestionDifficulty.Easy, ("Дорога", true), ("Порог", false), ("Ворота", false)),
                CreateQuestion("ЦIунна", QuestionDifficulty.Hard, ("Хлебница", true), ("Сундук", false), ("Окно", false)),
                CreateQuestion("Шал", QuestionDifficulty.Medium, ("Свет", false), ("Стена", true), ("Пол", false)),
                CreateQuestion("ГIиникь", QuestionDifficulty.Hard, ("Забор", false), ("Беседка у дороги", true), ("Ворота", false)),
                CreateQuestion("ЦIа", QuestionDifficulty.Easy, ("Лампа", false), ("Огонь", true), ("Печь", false)),
                CreateQuestion("Ккиркк", QuestionDifficulty.Hard, ("Ключ", false), ("Засов двери", true), ("Порог", false))
            }
        },
        new CategorySeedItem
        {
            Name = "Растения и плоды",
            NameDargwa = "ГIяшлар ва кьар",
            Description = "Растения, плоды и материалы",
            Questions = new()
            {
                CreateQuestion("Мурхь", QuestionDifficulty.Easy, ("Яблоко", true), ("Груша", false), ("Слива", false)),
                CreateQuestion("ГIулрухъи", QuestionDifficulty.Hard, ("Горькие плоды дикой вишни", true), ("Малина", false), ("Ежевика", false)),
                CreateQuestion("Мижи", QuestionDifficulty.Medium, ("Чеснок", false), ("Лук", true), ("Морковь", false)),
                CreateQuestion("ГIукI", QuestionDifficulty.Hard, ("Земляной съедобный орешек", true), ("Морковь", false), ("Репа", false)),
                CreateQuestion("КьикIва", QuestionDifficulty.Medium, ("Зрелый плод", false), ("Степень созревания плода", true), ("Гнилой плод", false)),
                CreateQuestion("ЦIук", QuestionDifficulty.Hard, ("Трава", false), ("Солома", true), ("Цветы", false)),
                CreateQuestion("КьяртIри", QuestionDifficulty.Medium, ("Стручки фасоли", true), ("Листья", false), ("Ветки", false)),
                CreateQuestion("Кьям-кьям хъара", QuestionDifficulty.Hard, ("Фасоль", false), ("Дикий горошек", true), ("Чечевица", false)),
                CreateQuestion("Нукьун", QuestionDifficulty.Medium, ("Тесто", false), ("Пирог", false), ("Мука", true)),
                CreateQuestion("ГIянкIа", QuestionDifficulty.Hard, ("Корень", false), ("Свежая кора дерева", true), ("Лист", false))
            }
        },
        new CategorySeedItem
        {
            Name = "Люди и качества",
            NameDargwa = "Адамти ва къалип",
            Description = "Люди, роли и личные качества",
            Questions = new()
            {
                CreateQuestion("Узи", QuestionDifficulty.Easy, ("Брат", true), ("Друг", false), ("Сосед", false)),
                CreateQuestion("ГIицIкиухули", QuestionDifficulty.Hard, ("Добрый человек", false), ("Приставучий человек", true), ("Смелый человек", false)),
                CreateQuestion("Хьунул", QuestionDifficulty.Easy, ("Девушка/Жена", true), ("Мать", false), ("Сестра", false)),
                CreateQuestion("Кархван", QuestionDifficulty.Hard, ("Богач", false), ("Просящий милостыню", true), ("Купец", false)),
                CreateQuestion("Адам", QuestionDifficulty.Easy, ("Человек", true), ("Мужчина", false), ("Ребенок", false)),
                CreateQuestion("ГIянтIикIа", QuestionDifficulty.Hard, ("Маленький ребенок", false), ("Красивая женщина/одежда", true), ("Старушка", false)),
                CreateQuestion("ХIядур", QuestionDifficulty.Medium, ("Готов", true), ("Устал", false), ("Заболел", false)),
                CreateQuestion("ГIяркка", QuestionDifficulty.Hard, ("Здоровый", false), ("Долго болеющий", true), ("Быстрый", false)),
                CreateQuestion("Усси", QuestionDifficulty.Medium, ("Маленький", true), ("Большой", false), ("Средний", false)),
                CreateQuestion("Кузиба", QuestionDifficulty.Hard, ("Мудрец", false), ("Проказник", true), ("Помощник", false))
            }
        },
        new CategorySeedItem
        {
            Name = "Разное и абстрактное",
            NameDargwa = "Жура-журала",
            Description = "Абстрактные и общие понятия",
            Questions = new()
            {
                CreateQuestion("Баркалла", QuestionDifficulty.Easy, ("Пожалуйста", false), ("Спасибо", true), ("Извините", false)),
                CreateQuestion("Вачар-чакар", QuestionDifficulty.Hard, ("Праздник", false), ("Купля-продажа", true), ("Спор", false)),
                CreateQuestion("ГIяхIил", QuestionDifficulty.Medium, ("Плохо", false), ("Хорошо", true), ("Быстро", false)),
                CreateQuestion("Кашки", QuestionDifficulty.Hard, ("Никогда", false), ("Если б так было", true), ("Наверное", false)),
                CreateQuestion("Гье", QuestionDifficulty.Easy, ("Да", true), ("Нет", false), ("Может", false)),
                CreateQuestion("Кихьри", QuestionDifficulty.Hard, ("Посылка", false), ("Послание", true), ("Сказка", false)),
                CreateQuestion("ХIебалас", QuestionDifficulty.Medium, ("Не знаю", true), ("Вижу", false), ("Слышу", false)),
                CreateQuestion("ВасвасикIни", QuestionDifficulty.Hard, ("Уверенность", false), ("Сомнения", true), ("Радость", false)),
                CreateQuestion("Агьари", QuestionDifficulty.Easy, ("Иди", false), ("Сюда", false), ("Нет", true)),
                CreateQuestion("КьяртIи", QuestionDifficulty.Hard, ("Радость", false), ("Скорбь/похороны", true), ("Свадьба", false))
            }
        }
    };

    private static QuestionSeedItem CreateQuestion(
        string text,
        QuestionDifficulty difficulty,
        (string answer, bool isCorrect) first,
        (string answer, bool isCorrect) second,
        (string answer, bool isCorrect) third)
    {
        return new QuestionSeedItem
        {
            Text = text,
            Difficulty = difficulty,
            Answers = new()
            {
                new AnswerSeedItem { Text = first.answer, IsCorrect = first.isCorrect },
                new AnswerSeedItem { Text = second.answer, IsCorrect = second.isCorrect },
                new AnswerSeedItem { Text = third.answer, IsCorrect = third.isCorrect }
            }
        };
    }
}