namespace LDS.Web.Shared.ViewModels;

public class LeaderboardViewModel
{
    public string? Year { get; set; }
    public DateTime? LastUpdated { get; set; }
    public IEnumerable<LeaderboardRunnerViewModel>? Men { get; set; }
    public IEnumerable<LeaderboardRunnerViewModel>? Women { get; set; }
}

public class LeaderboardRunnerViewModel
{
    public string? Name { get; set; }
    public int Id { get; set; }
    public decimal Miles { get; set; }
    public string? Position { get; set; }
}