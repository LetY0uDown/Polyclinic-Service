using Database.Repositories;
using Models;

namespace Database.Services;

public interface ISpecialityService
{
    IRepository<Speciality> Repository { get; }

    Task<Speciality?> FindAsync (SpecialityID id);
}