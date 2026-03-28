namespace LDS.Web.Admin.ViewModels;

public class RaceParticipationByRunner
{
    public int RunnerId { get; set; }

    public string? RunnerName { get; set; }

    public List<RaceParticipationByRunnerEntry>? Races { get; set; }

    public decimal TotalMiles
    {
        get
        {
            if (Races == null)
            {
                return 0;
            }
            return Races.Sum(r => r.Miles);
        }
    }
}

public class RaceParticipationByRunnerEntry
{
    public int RaceId { get; set; }

    public string? RaceName { get; set; }

    public DateOnly Date { get; set; }

    public decimal Miles { get; set; }
}