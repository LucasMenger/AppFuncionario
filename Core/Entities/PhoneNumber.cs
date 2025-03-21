using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class PhoneNumber
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(20)]
    public string Number { get; set; }

    [ForeignKey("Employee")]
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
}