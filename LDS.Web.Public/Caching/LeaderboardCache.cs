using LDS.Data.Models;
using LDS.Data.Services.Interfaces;
using LDS.Web.Public.Extensions;
using LDS.Web.Public.ViewModels;
using Microsoft.Extensions.Caching.Memory;

namespace LDS.Web.Public.Caching;

public interface ILeaderboardCache
{
    public IEnumerable<LeaderboardRunnerViewModel> GetLeaderboardRunners(string gender, int? take);
    public IEnumerable<LeaderboardRunnerViewModel> GetLeaderboardRunners(string gender);
    public IEnumerable<LeaderboardRunnerViewModel> GetYearLeaderboard(string gender, int year);
}

public class LeaderboardCache (ITotalMilesService totalMilesService, IMemoryCache cache) : ILeaderboardCache
{
    public IEnumerable<LeaderboardRunnerViewModel> GetLeaderboardRunners(string gender)
    {
        return  GetLeaderboardRunners(gender, null); 
    }
    
    public IEnumerable<LeaderboardRunnerViewModel> GetLeaderboardRunners(string gender, int? take = null)
    {
        var cacheKey = $"Leaderboard_top10_{gender}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<LeaderboardRunnerViewModel> runners))
        {
            var result= totalMilesService.GetLeaderboard(gender);
            if (take != null)
            {
                result = result.Take(take.Value);
            }
            runners = result.Select(r => new LeaderboardRunnerViewModel
                {
                    Name = r.FirstName + " " + r.LastName,
                    Id = r.RunnerId,
                    Miles = r.Miles
                })
                .ToList()
                .WithPositions();

            cache.Set(cacheKey, runners);
        }
        
        return runners;
    }
    
    public IEnumerable<LeaderboardRunnerViewModel> GetYearLeaderboard(string gender, int year)
    {
        IQueryable<TotalMilesBase> queryable;
        switch (year)
        {
            case 2026: queryable = totalMilesService.GetLeaderboard(gender); break;
            case 2025: queryable = totalMilesService.GetLeaderboard2025(gender); break;
            default: throw new ArgumentException("Unknown year " + year);
        }

        var cacheKey = $"Leaderboard_{gender}_{year}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<LeaderboardRunnerViewModel> runners))
        {
            runners = queryable.Select(r => new LeaderboardRunnerViewModel
                {
                    Name = r.FirstName + " " + r.LastName,
                    Id = r.RunnerId,
                    Miles = r.Miles
                })
                .ToList()
                .WithPositions();
            
            cache.Set(cacheKey, runners);
        }

        return runners;
    }

}