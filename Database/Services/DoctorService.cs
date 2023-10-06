using Database.Repositories;
using Models;
using Models.IDs;

namespace Database.Services;

public sealed class DoctorService : IDoctorService
{
    public DoctorService(IRepository<Doctor> repository)
    {
        Repository = repository;
    }

    public IRepository<Doctor> Repository { get; }

    public async Task<Doctor?> FindAsync(DoctorID id)
    {
        return await Repository.FirstOrDefaultAsync(x => x.ID == id);
    }
}