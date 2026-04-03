using LDS.Data.Models;
using LDS.Data.Services.Interfaces;

namespace LDS.Data.Services;

public class TotalMilesService (LdsContext ldsContext) : ITotalMilesService
{
    public IQueryable<TotalMiles> GetLeaderboard(string gender)
    {
        return ldsContext.TotalMiles
            .Where(r => r.Gender == gender)
            .OrderByDescending(r => r.Miles)
            .ThenBy(r => r.LastName)
            .ThenBy(r => r.FirstName);
    }
    
    public IQueryable<TotalMiles2025> GetLeaderboard2025(string gender)
    {
        return ldsContext.TotalMiles2025
            .Where(r => r.Gender == gender)
            .OrderByDescending(r => r.Miles)
            .ThenBy(r => r.LastName)
            .ThenBy(r => r.FirstName);
    }

}