using LDS.Data.Models;

namespace LDS.Data.Services.Interfaces;

public interface IRaceEntryService
{
    public RaceEntry? Get(int raceId, int runnerId);
    public Task<bool> UpdateMiles(int raceId, int runnerId, decimal miles);
    public Task<bool> UpdateRunner(int oldRunnerId, int newRunnerId);
}