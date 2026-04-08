namespace LDS.Web.Shared.Models;

public record CacheInvalidationDetail
{
    public CacheInvalidationAction Action { get; init; }
    public string? RunnerId { get; init; }
    public string? RaceId { get; init; }
    public string? Year { get; init; }
    public string? Gender { get; set; }
}

public enum CacheInvalidationType
{
    Runner,
    RaceParticipationForRunner,
    RaceParticipationForRace,
    LeaderboardForYear,
    RacesForYear,
    AllLeaderboards
}

public enum CacheInvalidationAction
{
    AddRunnersToRace,
    RunnerConsolidation,
    EditRunner,
    EditRaceEntry
}