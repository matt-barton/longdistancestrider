using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LDS.Data.Models;

[Table("Parameters")]
public class Parameter
{
    [Key]
    public string Name { get; set; }
    public string Value { get; set; }
}