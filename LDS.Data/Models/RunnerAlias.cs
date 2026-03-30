using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LDS.Data.Models;

[Table("RunnerAlias")]
public class RunnerAlias
{
    public int RunnerId { get; set; }

    [Key]
    public string? Alias { get; set; }
}