using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq.Expressions;

namespace Database.Repositories;

public sealed class SpecialityRepository : IRepository<Speciality>
{
    private readonly PolyclinicContext _context;

    public SpecialityRepository (PolyclinicContext context)
    {
        _context = context;
    }

    public async Task<Speciality> AddAsync (Speciality entity)
    {
        await _context.Specialities.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> AnyAsync (Expression<Func<Speciality, bool>> func)
    {
        return await _context.Specialities.AnyAsync(func);
    }

    public async Task DeleteAsync (Speciality entity)
    {
        _context.Specialities.Entry(entity).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
    }

    public async Task<Speciality?> FindAsync (object id)
    {
        return await FirstOrDefaultAsync(s => s.ID == (Guid)id);
    }

    public async Task<Speciality?> FirstOrDefaultAsync (Expression<Func<Speciality, bool>> func)
    {
        return await _context.Specialities
                             .Include(s => s.Doctors)
                             .FirstOrDefaultAsync(func);
    }

    public async Task<List<Speciality>> GetAllAsync ()
    {
        return await _context.Specialities
                             .Include(s => s.Doctors)
                             .ToListAsync();
    }

    public async Task UpdateAsync (Speciality entity)
    {
        _context.Specialities.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<List<Speciality>> WhereAsync (Expression<Func<Speciality, bool>> func)
    {
        return await _context.Specialities.Where(func).ToListAsync();
    }
}