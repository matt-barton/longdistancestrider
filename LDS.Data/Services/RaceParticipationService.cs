using LDS.Data.Models;
using LDS.Data.Services.Interfaces;

namespace LDS.Data.Services;

public class RaceParticipationService(LdsContext ldsContext) : IRaceParticipationService
{
    public IEnumerable<RaceParticipation> GetForRunner(int runnerId)
    {
        return ldsContext.RacePartipation
            .Where(r => r.RunnerId == runnerId)
            .OrderByDescending(r => r.Date)
            .ToList();
    }
    
    public IEnumerable<RaceParticipation> GetForRace(int raceId)
    {
        return ldsContext.RacePartipation
            .Where(r => r.RaceId == raceId)
            .OrderByDescending(r => r.Date)
            .ThenBy(r => r.RunnerName)
            .ToList();
    }

}