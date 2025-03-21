using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Employee
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("nome")]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        [Column("sobrenome")]
        public string Sobrenome { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [StringLength(11)]
        [Column("cpf")]
        public string CPF { get; set; }

        [StringLength(15)] 
        [Column("telefone")]

        public string Telefone { get; set; }

        [Required]
        [StringLength(255)] 
        [Column("senha")]
        public string Senha { get; set; }

        [Column("gerenteid")]
        public int? GerenteId { get; set; }

        [ForeignKey("GerenteId")]
        public Employee Gerente { get; set; }

        [Required]
        [Column("permissao")]

        public string Permissao { get; set; } // Enum para definir o nível de permissão
    }
}