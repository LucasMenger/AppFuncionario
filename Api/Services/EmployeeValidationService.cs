using System.ComponentModel.DataAnnotations;
using Api.Data;
using Core.Enums;
using Core.Requests;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class EmployeeValidationService
{
    private readonly AppDbContext context;

    public EmployeeValidationService(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<List<string>> ValidarFuncionarioAsync(CreateEmployeeRequest request, int? funcionarioId = null)
    {
        var erros = new List<string>();

        // Validação de nome e sobrenome (obrigatório)
        if (string.IsNullOrWhiteSpace(request.Nome) || string.IsNullOrWhiteSpace(request.Sobrenome))
        {
            erros.Add("Nome e Sobrenome são obrigatórios.");
        }

        // Validação de e-mail (obrigatório)
        if (string.IsNullOrWhiteSpace(request.Email) || !new EmailAddressAttribute().IsValid(request.Email))
        {
            erros.Add("E-mail é obrigatório e precisa ser válido.");
        }

        // Validação de CPF (único e obrigatório)
        if (string.IsNullOrWhiteSpace(request.CPF))
        {
            erros.Add("Número de documento (CPF) é obrigatório.");
        }
        else if (await context.Employees.AnyAsync(e => e.CPF == request.CPF && e.Id != funcionarioId))
        {
            erros.Add("O CPF já está cadastrado.");
        }

        // // Validação de maioridade
        // if (DateTime.TryParse(request.DataNascimento, out var dataNascimento) && dataNascimento.AddYears(18) > DateTime.Today)
        // {
        //     erros.Add("O funcionário não pode ser menor de idade.");
        // }

        // Validação de permissões (não pode criar usuário com permissões superiores)
        if (funcionarioId.HasValue)
        {
            var funcionario = await context.Employees.FindAsync(funcionarioId.Value);
            if (funcionario != null && !FuncionarioPodeCriarFuncionarioComPermissaoSuperior(funcionario.Permissao, request.Permissao))
            {
                erros.Add("O usuário não pode criar outro com permissões superiores a suas.");
            }
        }

        return erros;
    }

    private bool FuncionarioPodeCriarFuncionarioComPermissaoSuperior(string permissaoCriador, Permissao permissaoCriado)
    {
        var permissoesHierarquia = new Dictionary<string, int>
        {
            { "Funcionario", 1 },
            { "Lider", 2 },
            { "Diretor", 3 }
        };

        return permissoesHierarquia[permissaoCriador] >= permissoesHierarquia[permissaoCriado.ToString()];
    }
}