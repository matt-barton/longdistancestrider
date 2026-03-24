using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Parameters")]
public class Parameter
{
    [Key]
    public string Name { get; set; }
    public string Value { get; set; }
}