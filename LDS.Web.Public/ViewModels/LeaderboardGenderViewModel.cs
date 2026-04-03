namespace LDS.Web.Public.ViewModels;

public class LeaderboardGenderViewModel
{
    public string Gender { get; set; }
    public int Year { get; set; }
    public int? PreviousYear { get; set; }
    public int? NextYear { get; set; }
    public string StandingsType { get; set; } = "";
    public DateTime? LastUpdated { get; set; }
    public IEnumerable<LeaderboardRunnerViewModel>? Runners { get; set; }
}
