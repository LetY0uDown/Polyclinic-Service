using Database.Repositories;
using Models;
using Tools;

namespace Database.Services;

/// <summary>
/// Интерфейс для упрощения действий с БД. Я устал, мне лень писать описания дальше :(
/// </summary>
public interface IClientService
{
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
    /// <returns>Существует ли клиент (true/false) и сообщение об ошибке, если клиент не существует</returns>
    Task<DataErrorUnion<bool>> CheckIfClientExistAsync (string email, string login);

    IRepository<Client> Repository { get; }
}