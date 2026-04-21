# Dargwa Quiz Bot

Telegram-бот для изучения даргинского языка через викторины.

## Возможности

### Команды
- `/start` — регистрация и приветствие.
- `/help` — список команд.
- `/quiz` — начать викторину.
- `/cancel` — отменить активную викторину.
- `/stats` — личная статистика.
- `/leaderboard` — топ игроков.
- `/achievements` (и `/achivments`) — открытые достижения.
- `/language` — смена языка интерфейса.

### Игровой процесс
- Одна викторина: 10 вопросов.
- Выбор категории или режим «Все категории».
- Вопросы берутся случайно среди активных (`IsActive = true`).
- Ответы — через inline-кнопки Telegram.
- В конце показываются правильные ответы, точность, очки и время.

### Подсчет очков
Базовые очки за вопрос:
- Easy: 10
- Medium: 20
- Hard: 30

Бонус за скорость:
- < 10 секунд: +50%
- < 20 секунд: +25%
- >= 20 секунд: без бонуса

### Локализация
- Русский и даргинский языки интерфейса.
- Язык хранится в профиле пользователя в БД.

## Архитектура

- `Handlers/`
  - `CommandHandler` — команды.
  - `CallbackQueryHandler` — inline-callback.
  - `QuizHandler` — сценарий викторины.
- `Services/Implementations/`
  - `UserService`, `QuizService`, `StatisticsService`, `LocalizationService`, `TelegramBotService`.
- `Data/`
  - `QuizDbContext`, `DbInitializer`, `Seeding/*`.
- `Models/`, `DTOs/`, `Enums/`.
- `Migrations/`.

## БД (PostgreSQL + EF Core)

### Таблицы
- `Users`
- `Categories`
- `Questions`
- `Answers`
- `QuizSessions`
- `UserAnswers`
- `Achievements`
- `UserAchievements`

### Связи
- `User -> QuizSessions` (1:N)
- `User -> UserAnswers` (1:N)
- `Category -> Questions` (1:N)
- `Question -> Answers` (1:N)
- `QuizSession -> UserAnswers` (1:N)
- `User <-> Achievements` (M:N через `UserAchievements`)

### Миграции и сидирование
- При старте выполняется `context.Database.Migrate()`.
- Затем инициализируются данные:
  - `AchievementsSeeder`
  - `CategoriesSeeder` + `CategoriesSeedCatalog`
- Сидирование:
  - обновляет существующие данные,
  - пересобирает ответы для вопросов,
  - отключает устаревшие вопросы (`IsActive = false`).

## Контент

Категории и вопросы хранятся в `Data/Seeding/CategoriesSeedCatalog.cs`.

## Технологии
- .NET 8 / ASP.NET Core
- Telegram.Bot
- Entity Framework Core 8
- PostgreSQL (Npgsql)
- Swagger (dev)

## Локальный запуск

### Требования
- .NET SDK 8
- PostgreSQL 14+
- Telegram Bot Token

### Обязательные настройки
- `ConnectionStrings:DefaultConnection`
- `TelegramBot:Token`

Пример env:

```bash
export ConnectionStrings__DefaultConnection="Host=localhost;Port=5432;Database=dargwa_quiz;Username=postgres;Password=postgres"
export TelegramBot__Token="YOUR_TELEGRAM_BOT_TOKEN"
