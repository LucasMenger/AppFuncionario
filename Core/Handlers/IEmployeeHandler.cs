using Core.Entities;
using Core.Requests;
using Core.Responses;

namespace Core.Handlers;

public interface IEmployeeHandler
{
    Task<Response<Employee?>> CreateAsync(CreateEmployeeRequest request);
    Task<Response<Employee?>> UpdateAsync(UpdateEmployeeRequest request);
    Task<Response<Employee?>> DeleteAsync(DeleteEmployeeRequest request);
    Task<Response<Employee?>> GetByIdAsync(GetByIdEmployeeRequest request);

    Task<PagedResponse<List<Employee>>> GetAllAsync( GetAllEmployeeRequest request);
}