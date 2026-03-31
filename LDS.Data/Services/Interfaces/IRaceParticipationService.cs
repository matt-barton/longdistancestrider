using LDS.Data.Models;

namespace LDS.Data.Services.Interfaces;

public interface IRaceParticipationService
{
    public IEnumerable<RaceParticipation> GetForRunner(int runnerId);
    public IEnumerable<RaceParticipation> GetForRunner(int runnerId, int year);
    public IEnumerable<RaceParticipation> GetForRace(int raceId);
}