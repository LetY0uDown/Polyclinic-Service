using Database.Repositories;
using Models;
using Models.IDs;

namespace Database.Services;

public interface IDoctorService
{
    public IRepository<Doctor> Repository { get; }

    public Task<Doctor?> FindAsync (DoctorID id);
}