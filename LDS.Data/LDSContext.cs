using LDS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LDS.Data;

public class LdsContext(DbContextOptions<LdsContext> options) : DbContext(options)
{
    public DbSet<Runner> Runners { get; set; }
    public DbSet<Race> Races { get; set; }
    public DbSet<RaceEntry> RaceEntries { get; set; }
    public DbSet<RaceParticipation> RacePartipation { get; set; }
    public DbSet<TotalMiles> TotalMiles { get; set; }
    public DbSet<TotalMiles2025> TotalMiles2025 { get; set; }
    public DbSet<RunnerAlias> RunnerAliases { get; set; }
    public DbSet<Parameter> Parameters { get; set; }
}