using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq.Expressions;

namespace Database.Repositories;

public sealed class ScheduleRepository : IRepository<Schedule>
{
    private readonly PolyclinicContext _context;

    public ScheduleRepository (PolyclinicContext context)
    {
        _context = context;
    }

    public async Task<Schedule> AddAsync (Schedule entity)
    {
        await _context.Schedules.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public Task<bool> AnyAsync (Expression<Func<Schedule, bool>> func)
    {
        return _context.Schedules.Include(s => s.Client)
                                 .Include(s => s.Doctor)
                                 .Include(s => s.Status)
                                 .AnyAsync(func);
    }

    public async Task DeleteAsync (Schedule entity)
    {
        _context.Schedules.Entry(entity).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
    }

    public async Task<Schedule?> FindAsync (int id)
    {
        return await FirstOrDefaultAsync(s => s.ID == id);
    }

    public Task<Schedule?> FirstOrDefaultAsync (Expression<Func<Schedule, bool>> func)
    {
        return _context.Schedules.Include(s => s.Client)
                                 .Include(s => s.Doctor)
                                 .Include(s => s.Status)
                                 .FirstOrDefaultAsync(func);
    }

    public Task<List<Schedule>> GetAllAsync ()
    {
        return _context.Schedules.Include(s => s.Client)
                                 .Include(s => s.Doctor)
                                 .Include(s => s.Status)
                                 .ToListAsync();
    }

    public async Task UpdateAsync (Schedule entity)
    {
        _context.Schedules.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public Task<List<Schedule>> WhereAsync (Expression<Func<Schedule, bool>> func)
    {
        return _context.Schedules.Include(s => s.Client)
                                 .Include(s => s.Doctor)
                                 .ThenInclude(d => d.Speciality)
                                 .Include(s => s.Status)
                                 .Where(func)
                                 .ToListAsync();
    }
}