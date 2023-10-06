using Database.Repositories;
using Models;
using Models.IDs;

namespace Database.Services;

public sealed class SpecialityService : ISpecialityService
{
    public SpecialityService(IRepository<Speciality> repository)
    {
        Repository = repository;
    }

    public IRepository<Speciality> Repository { get; }

    public async Task<Speciality?> FindAsync(SpecialityID id)
    {
        return await Repository.FirstOrDefaultAsync(s => s.ID == id);
    }
}