using System.ComponentModel.DataAnnotations;

namespace LDS.Web.Public.ViewModels;

public class RaceViewModel
{
    public string Name { get; set; } = "";
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public IEnumerable<RaceRunnerViewModel> Runners { get; set; }
}