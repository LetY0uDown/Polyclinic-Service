using Database.Models;
using Database.Repositories;

namespace Database.Services;

public sealed class ClientService : IClientService
{
    public ClientService (IRepository<Client> repository)
    {
        Repository = repository;
    }

    public IRepository<Client> Repository { get; init; }

    public async Task<Client?> FindByEmailAsync (string email)
    {
        return await Repository.FirstOrDefaultAsync(c => c.Email == email);
    }
}