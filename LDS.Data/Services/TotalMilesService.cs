using LDS.Data.Models;
using LDS.Data.Services.Interfaces;

namespace LDS.Data.Services;

public class TotalMilesService (LdsContext ldsContext) : ITotalMilesService
{
    public IEnumerable<TotalMiles> GetLeaderboard(string gender)
    {
        return ldsContext.TotalMiles
            .Where(r => r.Gender == gender)
            .OrderByDescending(r => r.Miles)
            .ThenBy(r => r.LastName)
            .ThenBy(r => r.FirstName);
    }
}