using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LDS.Data.Models;

[Table("vRaceParticipation")]
[PrimaryKey(nameof(RaceId), nameof(RunnerId))]
public class RaceParticipation
{

    public int RaceId { get; set; }

    public int RunnerId { get; set; }

    public string? RaceName { get; set; }

    public string? RunnerName { get; set; }
    
    public DateOnly Date { get; set; }

    public decimal Miles {get; set; }
}