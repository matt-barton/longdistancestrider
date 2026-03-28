using System.ComponentModel.DataAnnotations;

namespace LDS.Web.Admin.ViewModels;

public class RaceParticipationViewModel
{
    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Date { get; set; }

    public decimal Miles { get; set; }

    [Required]
    public string? Runners { get; set; }

}