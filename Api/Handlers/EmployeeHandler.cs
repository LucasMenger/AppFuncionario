using Api.Data;
using Api.Services;
using Core.Entities;
using Core.Handlers;
using Core.Requests;
using Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Api.Handlers;

public class EmployeeHandler (AppDbContext context) : IEmployeeHandler
{
    public async Task<Response<Employee?>> CreateAsync(CreateEmployeeRequest request)
    {
        try
        { 
            var validationService = new EmployeeValidationService(context);
            var erros = await validationService.ValidarFuncionarioAsync(request);
            if (erros.Any())
            {
                return new Response<Employee?>(null, 400, string.Join(" ", erros));
            }
            
            var employee = new Employee
            {
                Nome = request.Nome,
                Sobrenome = request.Sobrenome,
                Email = request.Email,
                CPF = request.CPF,
                Telefone = request.Telefone,
                Senha = HashHelper.GerarHashSenha(request.Senha),
                GerenteId = request.GerenteId,
                Permissao = ((Permissao)request.Permissao).ToString()
            };

            await context.AddAsync(employee);
            await context.SaveChangesAsync();
            
            return new Response<Employee?>(employee, 201, "Funcionário criado com sucesso.");

        }
        catch (DbUpdateException ex) 
        {
            Console.WriteLine($"[Erro de Banco] {ex.InnerException?.Message ?? ex.Message}");
            return new Response<Employee?>(null, 500, $"[NV003] - Erro ao salvar no banco: {ex.InnerException?.Message ?? ex.Message}");
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"[Erro Geral] {ex.Message}");
            return new Response<Employee?>(null, 500, $"[NV001] - Erro inesperado: {ex.Message}");
        }
    }

    public async Task<Response<Employee?>> UpdateAsync(UpdateEmployeeRequest request)
    {
        try
        {
            var employee = await context.Employees.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (employee is null)
                return new Response<Employee?>(null, 400, "[NAV005 ] Error Obter funcionario");
            
            employee.Nome = request.Nome;
            employee.Sobrenome = request.Sobrenome;
            employee.Email = request.Email;
            employee.CPF = request.CPF;
            employee.Telefone = request.Telefone;
            employee.Senha = HashHelper.GerarHashSenha(request.Senha);
            employee.GerenteId = request.GerenteId;
            employee.Permissao = ((Permissao)request.Permissao).ToString();

            context.Employees.Update(employee);
            await context.SaveChangesAsync();

            return new Response<Employee?>(null, 201, "[NAV006] Update OK");

        }
        catch
        {
            return new Response<Employee?>(null, 500, "[NAV007] Error Update");
        }
    }

    public async Task<Response<Employee?>> DeleteAsync(DeleteEmployeeRequest request)
    {
        try
        {
            var employee = await context.Employees.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (employee is null)
                return new Response<Employee?>(null, 400, "[NAV008 ] Error Obter funcionario");

            context.Employees.Remove(employee);
            await context.SaveChangesAsync();
            
            return new Response<Employee?>(null, 201, "[NAV009] Delete OK");
            
        }
        catch 
        {
            return new Response<Employee?>(null, 500, "[NAV010] Error Delete");
        }
    }
    

    public async Task<Response<Employee?>> GetByIdAsync(GetByIdEmployeeRequest request)
    {
        try
        {
            var employee = await context.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);

            return employee is null
                ? new Response<Employee?>(null, 404, "[NAV011] Error GetById")
                : new Response<Employee?>(employee);
        }
        catch 
        {
            return new Response<Employee?>(null, 500, "[NAV012] Error GetById");
        }
    }

    public async Task<PagedResponse<List<Employee>>> GetAllAsync(GetAllEmployeeRequest request)
    {
        try
        {
            var query = context
                .Employees
                .AsNoTracking()
                .OrderBy(x => x.Id);

            var employees = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Employee>>(employees, count, request.PageNumber, request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<Employee>>(null, 500, "[NAV - 002 ] - Erro Obter todos os Funcionarios");
        }
    }
}