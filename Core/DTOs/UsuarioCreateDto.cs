using Core.Enums;

namespace Core.DTOs;

public class UsuarioCreateDto
{
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Email { get; set; }
    public string NumeroDocumento { get; set; }
    public List<string> Telefones { get; set; }
    public string GerenteNome { get; set; }
    public string Senha { get; set; }
    public DateTime DataNascimento { get; set; }
    public Permissao Permissao { get; set; }
}