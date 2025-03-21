using Api.Controllers;
using Core.Entities;
using Core.Handlers;
using Core.Requests;
using Core.Responses;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests.Controller;

public class EmployeesControllerTests
{
  private readonly Mock<IEmployeeHandler> _mockEmployeeHandler;
        private readonly Mock<EmployeeService> _mockEmployeeService;
        private readonly Mock<ILogger<EmployeesController>> _mockLogger;
        private readonly EmployeesController _controller;

        public EmployeesControllerTests()
        {
            _mockEmployeeHandler = new Mock<IEmployeeHandler>();
            _mockEmployeeService = new Mock<EmployeeService>();
            _mockLogger = new Mock<ILogger<EmployeesController>>();
            _controller = new EmployeesController(_mockEmployeeService.Object, _mockEmployeeHandler.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task CreateEmployee_ShouldReturnOk_WhenEmployeeIsCreatedSuccessfully()
        {
            // Arrange
            var request = new CreateEmployeeRequest { /* Set properties as needed */ };
            var response = new Response<bool> { IsSuccess = true, Data = true };
            _mockEmployeeHandler.Setup(x => x.CreateAsync(It.IsAny<CreateEmployeeRequest>())).ReturnsAsync(new Response<Employee?>());

            // Act
            var result = await _controller.CreateEmployee(request);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, actionResult.StatusCode);
        }

        [Fact]
        public async Task CreateEmployee_ShouldReturnUnauthorized_WhenCreationFails()
        {
            // Arrange
            var request = new CreateEmployeeRequest { };
            var response = new Response<bool> { IsSuccess = false, Message = "Unauthorized" };
            _mockEmployeeHandler.Setup(x => x.CreateAsync(It.IsAny<CreateEmployeeRequest>())).ReturnsAsync(new Response<Employee?>());

            // Act
            var result = await _controller.CreateEmployee(request);

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(401, actionResult.StatusCode);
        }

        [Fact]
        public async Task UpdateEmployee_ShouldReturnOk_WhenUpdateIsSuccessful()
        {
            // Arrange
            var request = new UpdateEmployeeRequest { Id = 1, };
            var response = new Response<string> { IsSuccess = true, Message = "Updated successfully" };
            _mockEmployeeHandler.Setup(x => x.UpdateAsync(It.IsAny<UpdateEmployeeRequest>())).ReturnsAsync(new Response<Employee?>());

            // Act
            var result = await _controller.UpdateEmployee(1, request);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, actionResult.StatusCode);
        }

        [Fact]
        public async Task UpdateEmployee_ShouldReturnUnauthorized_WhenUpdateFails()
        {
            // Arrange
            var request = new UpdateEmployeeRequest { Id = 1, /* Set other properties */ };
            var response = new Response<string> { IsSuccess = false, Message = "Unauthorized" };
            _mockEmployeeHandler.Setup(x => x.UpdateAsync(It.IsAny<UpdateEmployeeRequest>())).ReturnsAsync(new Response<Employee?>());

            // Act
            var result = await _controller.UpdateEmployee(1, request);

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(401, actionResult.StatusCode);
        }

        [Fact]
        public async Task DeleteEmployee_ShouldReturnOk_WhenDeletionIsSuccessful()
        {
            // Arrange
            var response = new Response<string> { IsSuccess = true, Message = "Deleted successfully" };
            _mockEmployeeHandler.Setup(x => x.DeleteAsync(It.IsAny<DeleteEmployeeRequest>())).ReturnsAsync(new Response<Employee?>());

            // Act
            var result = await _controller.DeleteEmployee(1);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, actionResult.StatusCode);
        }

        [Fact]
        public async Task DeleteEmployee_ShouldReturnUnauthorized_WhenDeletionFails()
        {
            // Arrange
            var response = new Response<string> { IsSuccess = false, Message = "Unauthorized" };
            _mockEmployeeHandler.Setup(x => x.DeleteAsync(It.IsAny<DeleteEmployeeRequest>())).ReturnsAsync(new Response<Employee?>());

            // Act
            var result = await _controller.DeleteEmployee(1);

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(401, actionResult.StatusCode);
        }

        [Fact]
        public async Task GetEmployeeById_ShouldReturnOk_WhenEmployeeIsFound()
        {
            // Arrange
            var response = new Response<Employee> { IsSuccess = true, Data = new Employee { Id = 1, Nome = "John" } };
            _mockEmployeeHandler.Setup(x => x.GetByIdAsync(It.IsAny<GetByIdEmployeeRequest>())).ReturnsAsync(response);

            // Act
            var result = await _controller.GetEmployeeById(1);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, actionResult.StatusCode);
        }

        [Fact]
        public async Task GetEmployeeById_ShouldReturnNotFound_WhenEmployeeNotFound()
        {
            // Arrange
            var response = new Response<Employee> { IsSuccess = false, Message = "Employee not found" };
            _mockEmployeeHandler.Setup(x => x.GetByIdAsync(It.IsAny<GetByIdEmployeeRequest>()))!.ReturnsAsync(response);

            // Act
            var result = await _controller.GetEmployeeById(1);

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(401, actionResult.StatusCode);
        }

        [Fact]
        public async Task GetAllEmployees_ShouldReturnOk_WhenEmployeesAreFound()
        {
            // Arrange
            var response = new Response<List<Employee>> { IsSuccess = true, Data = new List<Employee> { new Employee { Id = 1, Nome = "John" } } };
            _mockEmployeeHandler.Setup(x => x.GetAllAsync(It.IsAny<GetAllEmployeeRequest>())).ReturnsAsync(new PagedResponse<List<Employee>>(response.Data));

            // Act
            var result = await _controller.GetAllEmployees(new GetAllEmployeeRequest());

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, actionResult.StatusCode);
        }

        [Fact]
        public async Task GetAllEmployees_ShouldReturnNotFound_WhenNoEmployeesFound()
        {
            // Arrange
            var response = new Response<List<Employee>> { IsSuccess = true, Data = new List<Employee>() };
            _mockEmployeeHandler.Setup(x => x.GetAllAsync(It.IsAny<GetAllEmployeeRequest>())).ReturnsAsync(new PagedResponse<List<Employee>>(response.Data));

            // Act
            var result = await _controller.GetAllEmployees(new GetAllEmployeeRequest());

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, actionResult.StatusCode);
        }

        [Fact]
        public async Task GetAllEmployees_ShouldReturnServerError_WhenExceptionOccurs()
        {
            // Arrange
            _mockEmployeeHandler.Setup(x => x.GetAllAsync(It.IsAny<GetAllEmployeeRequest>())).ThrowsAsync(new Exception("Some error"));

            // Act
            var result = await _controller.GetAllEmployees(new GetAllEmployeeRequest());

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, actionResult.StatusCode);
        }
}