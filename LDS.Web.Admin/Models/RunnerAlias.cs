using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LDS.Web.Admin.Models;

[Table("RunnerAlias")]
public class RunnerAlias
{
    public int RunnerId { get; set; }

    [Key]
    public string? Alias { get; set; }
}