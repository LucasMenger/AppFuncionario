using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Role
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(20)]
    public string Name { get; set; }

    public List<Employee> Employees { get; set; }
}