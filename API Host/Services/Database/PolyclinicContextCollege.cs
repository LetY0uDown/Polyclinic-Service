using Database;
using Microsoft.EntityFrameworkCore;

namespace API_Host.Services.Database;

public sealed class PolyclinicContextCollege : PolyclinicContext
{
    private readonly IConfiguration _config;

    public PolyclinicContextCollege(IConfiguration config)
    {
        _config = config;

        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
            return;

        optionsBuilder.UseSqlServer(_config["ConnectionStrings:College"]);
    }
}