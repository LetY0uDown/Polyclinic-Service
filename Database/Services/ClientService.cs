using Database.Repositories;
using Models;
using Tools;

namespace Database.Services;

public sealed class ClientService : IClientService
{
    public ClientService (IRepository<Client> repository)
    {
        Repository = repository;
    }

    public IRepository<Client> Repository { get; init; }

    public async Task<DataErrorUnion<bool>> CheckIfClientExistAsync (string email, string login)
    {
        if (await Repository.AnyAsync(c => c.Email == email)) {
            return new(true, "Данная электронная почта уже зарегистрирована в системе. Попробуйте ввести другую, или восстановить пароль к вашей учётной записи.");
        }

        if (await Repository.AnyAsync(c => c.Login == login)) {
            return new(true, "Клиент с данным логином уже существует в системе!");
        }

        return new(false);
    }

    public async Task<DataErrorUnion<Client>> FindByDataAsync (string email, string login)
    {
        var client = await Repository.FirstOrDefaultAsync(c => c.Login == login);

        if (client is null) {
            return new(null, "Клиент с таким логином не существует");
        }

        if (client.Email != email) {
            return new(null, "Клиента с таким E-Mail не существует");
        }

        return new(client);
    }
}