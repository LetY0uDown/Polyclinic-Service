using Database.Repositories;
using Models;
using Models.IDs;

namespace Database.Services;

public interface ISpecialityService
{
    IRepository<Speciality> Repository { get; }

    Task<Speciality?> FindAsync (SpecialityID id);
}