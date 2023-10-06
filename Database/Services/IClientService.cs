using Database.Repositories;
using Models;
using Models.IDs;
using Tools;

namespace Database.Services;

/// <summary>
/// Интерфейс для упрощения действий с БД. Я устал, мне лень писать описания дальше :(
/// </summary>
public interface IClientService
{
    string HashPassword (string password);

    /// <summary>
    /// Ищеть в БД клиента по его электронной почте и логину
    /// </summary>
    /// <param name="email"></param>
    /// <returns>Найденного клиента или null и сообщение об ошибке, если клиент всё таки null</returns>
    Task<DataErrorUnion<Client>> FindByDataAsync (string email, string login);

    /// <summary>
    /// Проверяет, существует ли клиент с данной почтой и логином
    /// </summary>
    /// <param name="email"></param>
    /// <param name="login"></param>
    /// <returns>Существует ли клиент (true/false) и сообщение об ошибке, если клиент не существует</returns>/// <summary>
    Task<DataErrorUnion<bool>> CheckIfClientExistAsync (string email, string login);

    /// Находит запись в таблице БД по её ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Найденную запись или null</returns>
    Task<Client?> FindAsync (ClientID id);

    IRepository<Client> Repository { get; }
}