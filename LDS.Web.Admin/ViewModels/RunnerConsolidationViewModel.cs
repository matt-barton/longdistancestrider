using LDS.Data.Models;

namespace LDS.Web.Admin.ViewModels;

public class RunnerConsolidationViewModel
{
    public List<Runner>? RunnerToKeep { get; set; }
    public List<Runner>? RunnerToRemove { get; set; }
    public int RunnerToKeepId { get; set; }
    public int RunnerToRemoveId { get; set; }
    public bool CreateAlias { get; set; }
    public List<RunnerRaceEntryViewModel>? EntriesMoved { get; set; }
    public string? RemovedRunnerName { get; set; }
    public int? RemovedRunnerId { get; set; }
    public string? KeptRunnerName { get; set; }
    public int? KeptRunnerId { get; set; }
    public bool AliasCreated { get; set ;}
}