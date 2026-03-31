namespace LDS.Web.Public.ViewModels;

public class LeaderboardGenderViewModel
{
    public string Gender { get; set; }
    public string? Year { get; set; }
    public string? PreviousYear { get; set; }
    public DateTime? LastUpdated { get; set; }
    public IEnumerable<LeaderboardRunnerViewModel>? Runners { get; set; }
}
