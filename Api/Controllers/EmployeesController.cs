using Core.Entities;
using Core.Handlers;
using Core.Requests;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
// [Authorize]
[ApiController]
public class EmployeesController(
    EmployeeService employeeService,
    IEmployeeHandler employeeRepository,
    ILogger<EmployeesController> logger)
    : ControllerBase
{
    private readonly EmployeeService _employeeService = employeeService;
    private readonly IEmployeeHandler _employeeHandler = employeeRepository;
    private readonly ILogger<EmployeesController> _logger = logger;
    
    
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequest createEmployeeRequest)
    {
        var response = await _employeeHandler.CreateAsync(createEmployeeRequest);

        if (response.IsSuccess)
        {
            return Ok(response.Data);
        }

        return StatusCode(401, response.Message);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeRequest request)
    {
        if (request == null || id != request.Id)
            return BadRequest("Dados inválidos.");

        var response = await _employeeHandler.UpdateAsync(request);

        if (response.IsSuccess)
        {
            return Ok(response.Message);
        }
        return StatusCode(401, response.Message);
    }
    // Endpoint para deletar um funcionário
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var request = new DeleteEmployeeRequest { Id = id };
        var response = await _employeeHandler.DeleteAsync(request);

        if (response.IsSuccess)
        {
            return Ok(response.Message);
        }

        return StatusCode(401, response.Message);
    }

    // Endpoint para obter um funcionário por ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        var request = new GetByIdEmployeeRequest { Id = id };
        var response = await _employeeHandler.GetByIdAsync(request);

        if (response.IsSuccess)
        {
            return Ok(response.Data);
        }

        return StatusCode(401, response.Message);
    }
    [HttpGet]
    public async Task<IActionResult> GetAllEmployees([FromQuery] GetAllEmployeeRequest request)
    {
        try
        {
            var response = await _employeeHandler.GetAllAsync(request);

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound("Nenhum funcionário encontrado.");
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[NAV - 003] - Erro ao obter todos os funcionários.");
            return StatusCode(500, "[NAV - 004] - Erro ao obter todos os funcionários.");
        }
    }
}