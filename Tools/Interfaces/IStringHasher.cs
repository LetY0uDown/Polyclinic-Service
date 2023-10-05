namespace Tools.Services.Interfaces;

/// <summary>
/// Интерфейс для шифрования строк
/// </summary>
public interface IStringHasher
{
    /// <summary>
    /// Шифрует строку
    /// </summary>
    /// <param name="value">Строка, которую нужно зашифровать</param>
    /// <param name="salt">Соль для шифрования. Повышает безопасность</param>
    /// <returns>Зашифрованная строка. Логично</returns>
    string Hash (string value, string salt);
}