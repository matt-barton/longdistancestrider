using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table("vTotalMiles")]
[PrimaryKey(nameof(RunnerId))]
public class TotalMiles
{
    public int RunnerId { get; set; }

    public decimal Miles { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public string? Gender { get; set; }
}