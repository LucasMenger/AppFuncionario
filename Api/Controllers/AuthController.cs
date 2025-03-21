using Api.DTOs;
using Api.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(AuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] RequestLogin request)
    {
        if (request.Email == "admin@email.com" && request.Password == "Senha123!")
        {
            var token = authService.GenerateToken("1", request.Email, "Admin");
            return Ok(new { Token = token });
        }

        return Unauthorized("Credenciais inválidas");
    }
}
