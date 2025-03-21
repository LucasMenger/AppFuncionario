using Core.Entities;
using Core.Requests;
using Core.Responses;

namespace Core.Interfaces;

public interface IEmployeeRepository
{
    Task<Response<Employee?>> CreateAsync(CreateEmployeeRequest request);
}