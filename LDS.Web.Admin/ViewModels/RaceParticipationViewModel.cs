using System.ComponentModel.DataAnnotations;
using LDS.Data.Models;

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
    public string? ErrorMessage { get; set; }
    public Race? LastAddedRace { get; set; }
}