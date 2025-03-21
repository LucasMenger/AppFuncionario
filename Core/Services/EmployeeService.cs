using System.Security.Cryptography;
using System.Text;
using Core.Entities;

namespace Core.Services;

public class EmployeeService()
{
    // private readonly IEmployeeRepository _repository = repository;
    
    // public async Task AddEmployeeAsync(Employee employee)
    // {
    //     if (await _repository.IsDocumentNumberUniqueAsync(employee.DocumentNumber) == false)
    //         throw new Exception("O número de documento já está em uso.");
    //
    //     if ((DateTime.UtcNow.Year - employee.DateOfBirth.Year) < 18)
    //         throw new Exception("O funcionário não pode ser menor de idade.");
    //
    //   
    //     employee.PasswordHash = HashPassword(employee.PasswordHash);
    //     await _repository.AddAsync(employee);
    // }
    //
    // private static string HashPassword(string password)
    // {
    //     using var sha256 = SHA256.Create();
    //     var bytes = Encoding.UTF8.GetBytes(password);
    //     var hash = sha256.ComputeHash(bytes);
    //     return Convert.ToBase64String(hash);
    // }
}