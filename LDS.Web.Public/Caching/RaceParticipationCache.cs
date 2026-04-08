using LDS.Data.Models;
using LDS.Data.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace LDS.Web.Public.Caching;

public interface IRaceParticipationCache
{
    public IEnumerable<RaceParticipation> GetForRace(int raceId);
    public IEnumerable<RaceParticipation> GetForRunner(int runnerId, int year);
}

public class RaceParticipationCache (IRaceParticipationService service, IMemoryCache cache) : IRaceParticipationCache
{
    public IEnumerable<RaceParticipation> GetForRace(int raceId)
    {
        var cacheKey = $"Race_Participation_For_Race_{raceId}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<RaceParticipation> raceParticipations))
        {
            raceParticipations = service.GetForRace(raceId);

            cache.Set(cacheKey, raceParticipations);
        }
        
        return raceParticipations;
    }
    public IEnumerable<RaceParticipation> GetForRunner(int runnerId, int year)
    {
        var cacheKey = $"Race_Participation_For_Runner_{runnerId}_{year}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<RaceParticipation> raceParticipations))
        {
            raceParticipations = service.GetForRunner(runnerId, year);

            cache.Set(cacheKey, raceParticipations);
        }
        
        return raceParticipations;
    }
}

