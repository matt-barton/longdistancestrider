using LDS.Data.Models;

namespace LDS.Web.Public.ViewModels;

public class RunnerViewModel
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? DisplayName { get; set; }
    public string? Gender { get; set; }
    public int Year { get; set; }
    public int? PreviousYear { get; set; }
    public int? NextYear { get; set; }
    public List<RunnerRaceEntryViewModel>? RaceEntries { get; set; }
    public decimal? TotalMiles
    {
        get
        {
            return RaceEntries?.Sum(re => re.Miles);
        }
    }

    public bool ChangesSaved { get; set; }

    public RunnerViewModel () {}

    public RunnerViewModel (Runner runnerDbModel)
    {
        Id = runnerDbModel.Id;
        FirstName = runnerDbModel.FirstName;
        LastName = runnerDbModel.LastName;
        DisplayName =runnerDbModel.FullName;
        Gender = runnerDbModel.Gender;
    }
}

public class RunnerRaceEntryViewModel
{
    public string? RaceName { get; set; }
    public int? RaceId { get; set; }
    public DateOnly Date { get; set; }
    public decimal Miles { get; set; }
}