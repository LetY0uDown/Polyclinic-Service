using Database.Repositories;
using Microsoft.Extensions.Configuration;
using Models;
using Tools;
using Tools.Services.Interfaces;

namespace Database.Services;

public sealed class ClientService : IClientService
{
    private readonly IStringHasher _stringHasher;
    private readonly IConfiguration _config;

    public ClientService (IRepository<Client> repository, IStringHasher stringHasher, IConfiguration config)
    {
        Repository = repository;
        _stringHasher = stringHasher;
        _config = config;
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

    public string HashPassword (string password)
    {
        return _stringHasher.Hash(password, _config["Salt:Password"]!);
    }
}