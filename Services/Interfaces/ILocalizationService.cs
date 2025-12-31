using DargwaQuiz.Enums;

namespace DargwaQuiz.Services.Interfaces;

public interface ILocalizationService
{
    string GetMessage(string key, UserLanguage language);
    string GetCommand(string command, UserLanguage language);
    string GetError(string errorKey, UserLanguage language);
}
