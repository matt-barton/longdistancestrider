namespace LDS.Web.Public.ViewModels;

public class RaceListViewModel
{
    public int Year { get; set; }
    public int? PreviousYear { get; set; }
    public int? NextYear { get; set; }
    public List<RaceViewModel> Races { get; set; }
}