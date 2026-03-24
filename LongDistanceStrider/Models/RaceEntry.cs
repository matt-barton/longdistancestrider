using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table("RaceEntry")]
[PrimaryKey(nameof(RunnerId), nameof(RaceId))]
public class RaceEntry
{
    public int RunnerId { get; set; }

    public int RaceId { get; set; }

    public decimal Miles { get; set; }
}