using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("RunnerAlias")]
public class RunnerAlias
{
    public int RunnerId { get; set; }

    [Key]
    public string? Alias { get; set; }
}