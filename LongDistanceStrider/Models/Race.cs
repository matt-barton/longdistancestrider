using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Race")]
public class Race
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }
    
    public DateOnly? Date { get; set; }
}