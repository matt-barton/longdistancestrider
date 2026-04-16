using LDS.Web.Shared.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;

namespace LDS.Web.Public.Caching;

public interface ICacheInvalidation
{
    public List<string> Invalidate(IEnumerable<CacheInvalidationDetail> details);
    public void InvalidateAll();
}

public class CacheInvalidation (IMemoryCache cache, IParameterCache parameterCache) : ICacheInvalidation
{
    public List<string> Invalidate(IEnumerable<CacheInvalidationDetail> details)
    {
        var keys = new List<string>();

        foreach (var detail in details)
        {

            switch (detail.Action)
            {
                case CacheInvalidationAction.EditRunner:
                    keys.Add($"Runner_{detail.RunnerId}");
                    keys.Add("Leaderboard_Top10_M");
                    keys.Add("Leaderboard_Top10_F");
                    keys = keys.Concat(CacheKeyForEachYear($"Leaderboard_{detail.Gender}")).ToList();
                    break;

                case CacheInvalidationAction.RunnerConsolidation:
                    keys = keys.Concat(CacheKeyForEachYear($"Race_Participation_For_Runner_{detail.RunnerId}")).ToList();
                    keys.Add($"Race_Participation_For_Race_{detail.RaceId}");
                    if (string.IsNullOrEmpty(detail.Gender))
                    {
                        keys.Add($"Leaderboard_Top10_M");
                        keys.Add($"Leaderboard_Top10_F");
                        keys = keys.Concat(CacheKeyForEachYear($"Leaderboard_M")).ToList();
                        keys = keys.Concat(CacheKeyForEachYear($"Leaderboard_F")).ToList();
                    }
                    else
                    {
                        keys.Add($"Leaderboard_Top10_{detail.Gender}");
                        keys = keys.Concat(CacheKeyForEachYear($"Leaderboard_{detail.Gender}")).ToList();
                    }
                    break;

                case CacheInvalidationAction.AddRunnersToRace:
                    keys = keys.Concat(CacheKeyForEachYear($"Race_Participation_For_Runner_{detail.RunnerId}")).ToList();
                    keys.Add($"Race_Participation_For_Race_{detail.RaceId}");
                    if (string.IsNullOrEmpty(detail.Gender))
                    {
                        keys.Add("Leaderboard_Top10_M");
                        keys.Add("Leaderboard_Top10_F");
                        keys.Add($"Leaderboard_M_{detail.Year}");
                        keys.Add($"Leaderboard_F_{detail.Year}");
                    }
                    else
                    {
                        keys.Add($"Leaderboard_Top10_{detail.Gender}");
                        keys.Add($"Leaderboard_{detail.Gender}_{detail.Year}");
                    }
                    keys.Add($"Race_All_{detail.Year}");
                    break;

                case CacheInvalidationAction.EditRaceEntry:
                    keys = keys.Concat(CacheKeyForEachYear($"Race_Participation_For_Runner_{detail.RunnerId}")).ToList();
                    keys.Add($"Race_Participation_For_Race_{detail.RaceId}");
                    if (string.IsNullOrEmpty(detail.Gender))
                    {
                        keys.Add("Leaderboard_Top10_M");
                        keys.Add("Leaderboard_Top10_F");
                        keys.Add($"Leaderboard_M_{detail.Year}");
                        keys.Add($"Leaderboard_F_{detail.Year}");
                    }
                    else
                    {
                        keys.Add($"Leaderboard_Top10_{detail.Gender}");
                        keys.Add($"Leaderboard_{detail.Gender}_{detail.Year}");
                    }
                    break;
            }
        }

        try
        {
            if (keys.Count != 0)
            {
                var remove = keys.Distinct().ToList();
                remove.ForEach(cache.Remove);
                return keys;
            }
        }
        catch (Exception e)
        {
            return new List<string>();
        }
        return new List<string>();
    }

    public void InvalidateAll()
    {
        ((MemoryCache)cache).Clear();
    }

    private List<string> CacheKeyForEachYear(string cacheKey)
    {
        var startYear =  parameterCache.GetFirstYear();
        var endYear = parameterCache.GetCurrentYear();
        var yearKeys = new List<string>();
        var x = startYear;
        while (x <= endYear)
        {
            var key = $"{cacheKey}_{x}";
            yearKeys.Add(key);
            x++;
        }
        return yearKeys;
    }
    
}