using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Runner")]
public class Runner
{
    [Key]
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }
    
    public string? Gender { get; set; }

    public string FullName
    {
        get
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}