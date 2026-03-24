public class RaceParticipationByRace
{

    public int RaceId { get; set; }

    public string? RaceName { get; set; }
    
    public DateOnly Date { get; set; }

    public List<RaceParticipationByRaceEntry>? Entries { get; set; }

    public int? Count {
        get
        {
            return this.Entries?.Count;    
        }
    }
}

public class RaceParticipationByRaceEntry
{
    public int RunnerId { get; set; }
    public string? RunnerName { get; set; }
    public decimal Miles { get; set; }
}