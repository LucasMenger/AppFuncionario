using Core.Enums;

namespace Core.Requests;

public class UpdateEmployeeRequest
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public string Telefone { get; set; }
    public string Senha { get; set; }
    public int? GerenteId { get; set; }
    public Permissao Permissao { get; set; }
}