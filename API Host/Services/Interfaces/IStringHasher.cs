namespace API_Host.Services.Interfaces;

/// <summary>
/// Интерфейс для шифрования строк
/// </summary>
public interface IStringHasher
{
    /// <summary>
    /// Шифрует строку
    /// </summary>
    /// <param name="value">Строка, которую нужно зашифровать</param>
    /// <returns>Зашифрованная строка. Логично</returns>
    string Hash(string value);
}