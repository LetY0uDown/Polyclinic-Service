using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq.Expressions;

namespace Database.Repositories;

public sealed class ClientRepository : IRepository<Client>
{
    private readonly PolyclinicContext _context;

    public ClientRepository (PolyclinicContext context)
    {
        _context = context;
    }

    public async Task<Client> AddAsync (Client entity)
    {
        await _context.Clients.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> AnyAsync (Expression<Func<Client, bool>> func)
    {
        return await _context.Clients.AnyAsync(func);
    }

    public async Task DeleteAsync (Client entity)
    {
        _context.Clients.Entry(entity).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
    }

    public async Task<Client?> FindAsync (int id)
    {
        return await FirstOrDefaultAsync(c => c.ID == id);
    }

    public async Task<Client?> FirstOrDefaultAsync (Expression<Func<Client, bool>> func)
    {
        return await _context.Clients.Include(c => c.Schedules).FirstOrDefaultAsync(func);
    }

    public async Task<List<Client>> GetAllAsync ()
    {
        return await _context.Clients.Include(c => c.Schedules).ToListAsync();
    }

    public async Task UpdateAsync (Client entity)
    {
        _context.Clients.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<List<Client>> WhereAsync (Expression<Func<Client, bool>> func)
    {
        return await _context.Clients.Where(func).ToListAsync();
    }
}