using LDS.Data.Models;
using LDS.Data.Services.Interfaces;
using LDS.Web.Public.ViewModels;
using Microsoft.Extensions.Caching.Memory;

namespace LDS.Web.Public.Caching;

public interface IRaceCache
{
    public IEnumerable<Race>? GetAll (int year);
    public Race? Get (int id);
}

public class RaceCache (IRaceService raceService, IMemoryCache cache) : IRaceCache
{
    public IEnumerable<Race>? GetAll(int year)
    {
        var cacheKey = $"Race_All_{year}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Race>? races))
        {
            races = raceService.GetAll(year);
            
            cache.Set (cacheKey, races);
        }

        return races;
    }

    public Race? Get(int id)
    {
        var cacheKey = $"Race_{id}";
        if (!cache.TryGetValue(cacheKey, out Race? race))
        {
            race = raceService.Get(id);
            
            cache.Set(cacheKey, race);
        }
        
        return race;
    }
}