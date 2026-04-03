using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LDS.Data.Models;

[Table("vTotalMiles")]
[PrimaryKey(nameof(RunnerId))]
public class TotalMiles : TotalMilesBase;

[Table("vTotalMiles2025")]
[PrimaryKey(nameof(RunnerId))]
public class TotalMiles2025 : TotalMilesBase;

public abstract class TotalMilesBase
{
    public int RunnerId { get; set; }

    public decimal Miles { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public string? Gender { get; set; }
}
