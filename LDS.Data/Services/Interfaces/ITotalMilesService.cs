using LDS.Data.Models;

namespace LDS.Data.Services.Interfaces;

public interface ITotalMilesService
{
    public IEnumerable<TotalMiles> GetLeaderboard(string gender);
}