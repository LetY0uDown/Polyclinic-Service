using Database.Repositories;
using Models;

namespace Database.Services;

/// <summary>
/// Интерфейс для упрощения действий с БД. Я устал, мне лень писать описания дальше :(
/// </summary>
public interface IClientService
{
    /// <summary>
    /// Ищеть в БД клиента по его электронной почте
    /// </summary>
    /// <param name="email"></param>
    /// <returns>Найденного клиента или null</returns>
    Task<Client?> FindByEmailAsync (string email);

    IRepository<Client> Repository { get; }
}