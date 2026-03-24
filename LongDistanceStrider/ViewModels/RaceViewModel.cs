using Microsoft.Extensions.Diagnostics.HealthChecks;

public class RaceViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateOnly? Date { get; set; }
    public List<RaceEntryViewModel>? Entries { get; set; }
    public decimal? TotalRunners
    {
        get
        {
            return Entries?.Count();
        }
    }

    public bool ChangesSaved { get; set; }
    
    public RaceViewModel () {}

    public RaceViewModel (Race raceDbModel)
    {
        Id = raceDbModel.Id;
        Name = raceDbModel.Name;
        Date = raceDbModel.Date;
    }
}

public class RaceEntryViewModel
{
    public string? RunnerName { get; set; }
    public int? RunnerId { get; set; }
    public decimal Miles { get; set; }
}