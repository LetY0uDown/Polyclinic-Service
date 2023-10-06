using Database.Repositories;
using Models;

namespace Database.Services;

public interface IDoctorService
{
    public IRepository<Doctor> Repository { get; }

    public Task<Doctor?> FindAsync (DoctorID id);
}