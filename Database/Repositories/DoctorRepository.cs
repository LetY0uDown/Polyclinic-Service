using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq.Expressions;

namespace Database.Repositories;

public sealed class DoctorRepository : IRepository<Doctor>
{
    private readonly PolyclinicContext _context;

    public DoctorRepository (PolyclinicContext context)
    {
        _context = context;
    }

    public async Task<Doctor> AddAsync (Doctor entity)
    {
        await _context.Doctors.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public Task<bool> AnyAsync (Expression<Func<Doctor, bool>> func)
    {
        return _context.Doctors.AnyAsync(func);
    }

    public async Task DeleteAsync (Doctor entity)
    {
        _context.Doctors.Entry(entity).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
    }

    public async Task<Doctor?> FirstOrDefaultAsync (Expression<Func<Doctor, bool>> func)
    {
        return await _context.Doctors.Include(d => d.Cabinet)
                                     .Include(d => d.Schedules)
                                     .Include(d => d.Speciality)
                                     .FirstOrDefaultAsync(func);
    }

    public async Task<List<Doctor>> GetAllAsync ()
    {
        return await _context.Doctors.Include(d => d.Cabinet)
                                     .Include(d => d.Schedules)
                                     .Include(d => d.Speciality)
                                     .ToListAsync();
    }

    public async Task UpdateAsync (Doctor entity)
    {
        _context.Doctors.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<List<Doctor>> WhereAsync (Expression<Func<Doctor, bool>> func)
    {
        return await _context.Doctors.Where(func).ToListAsync();
    }
}