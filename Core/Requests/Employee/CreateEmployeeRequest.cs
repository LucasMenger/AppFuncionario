using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Enums;
using Permissao = Core.Enums.Permissao;

namespace Core.Requests;

public class CreateEmployeeRequest
{
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public string Telefone { get; set; }
    public string Senha { get; set; }
    public int? GerenteId { get; set; }
    public Permissao Permissao { get; set; }
}
