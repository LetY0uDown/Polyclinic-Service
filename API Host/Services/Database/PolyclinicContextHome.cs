using Database;
using Microsoft.EntityFrameworkCore;

namespace API_Host.Services.Database;

public sealed class PolyclinicContextHome : PolyclinicContext
{
    private readonly IConfiguration _config;

    public PolyclinicContextHome (IConfiguration config)
    {
        _config = config;

        Database.EnsureCreated();
    }

    protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
            return;

        optionsBuilder.UseSqlServer(_config["ConnectionStrings:Home"]);
    }
}