using LDS.Data.Models;

namespace LDS.Data.Services.Interfaces;

public interface ITotalMilesService
{
    public IQueryable<TotalMiles> GetLeaderboard(string gender);
    public IQueryable<TotalMiles2025> GetLeaderboard2025(string gender);
}