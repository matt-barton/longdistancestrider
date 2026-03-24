using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

public class LDSContext : DbContext
{
    public DbSet<Runner> Runners { get; set; }
    public DbSet<Race> Races { get; set; }
    public DbSet<RaceEntry> RaceEntries { get; set; }
    public DbSet<RaceParticipation> RacePartipation { get; set; }
    public DbSet<TotalMiles> TotalMiles { get; set; }
    public DbSet<RunnerAlias> RunnerAliases { get; set; }
    public DbSet<Parameter> Parameters { get; set; }
    
    private readonly Settings _settings;

    public LDSContext (IOptions<Settings> settings)
    {
        _settings = settings.Value;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connectionString = _settings?.Database?.ConnectionString
            ?? throw new InvalidOperationException("Database connection string is not set.");
        options.UseSqlServer(connectionString);
    }
}