using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LDS.Web.Admin.Models;

[Table("Race")]
public class Race
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }
    
    public DateOnly? Date { get; set; }
}