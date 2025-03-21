using Core.Entities;
using Core.Services;
using Moq;

namespace Tests.Services;

public class EmployeeServiceTests
    {
        // private readonly Mock<IEmployeeRepository> _mockRepository;
        // private readonly EmployeeService _employeeService;
        //
        // public EmployeeServiceTests()
        // {
        //     _mockRepository = new Mock<IEmployeeRepository>();
        //     _employeeService = new EmployeeService(_mockRepository.Object);
        // }
        //
        // [Fact]
        // public async Task AddEmployeeAsync_ShouldAddEmployee_WhenValidData()
        // {
        //     // Arrange
        //     var creator = new Employee 
        //     { 
        //         Id = 1, 
        //         FirstName = "Admin", 
        //         LastName = "Admin", 
        //         Email = "admin@email.com",
        //         DocumentNumber = "11122233344",
        //         PasswordHash = "hashedpassword",
        //         Role = RoleType.Director,
        //         DateOfBirth = new DateTime(1980, 1, 1),
        //         CreatedAt = DateTime.UtcNow
        //     };
        //
        //     var newEmployee = new Employee 
        //     { 
        //         FirstName = "John", 
        //         LastName = "Doe", 
        //         Email = "john.doe@email.com", 
        //         DocumentNumber = "12345678900", 
        //         Role = RoleType.Employee,
        //         PasswordHash = "hashedpassword123",
        //         DateOfBirth = new DateTime(1990, 1, 1),
        //         CreatedAt = DateTime.UtcNow
        //     };
        //
        //     _mockRepository.Setup(repo => repo.AddAsync(newEmployee)).Returns(Task.CompletedTask);
        //
        //     // Act
        //     await _employeeService.AddEmployeeAsync(newEmployee);
        //
        //     // Assert
        //     _mockRepository.Verify(repo => repo.AddAsync(newEmployee), Times.Once);
        // }
        //
        // [Fact]
        // public async Task AddEmployeeAsync_ShouldThrowException_WhenCreatorHasLowerRole()
        // {
        //     // Arrange
        //     var creator = new Employee 
        //     { 
        //         Id = 2, 
        //         FirstName = "John", 
        //         LastName = "Doe", 
        //         Email = "john.doe@email.com",
        //         DocumentNumber = "98765432100",
        //         PasswordHash = "hashedpassword123",
        //         Role = RoleType.Leader,
        //         DateOfBirth = new DateTime(1990, 1, 1),
        //         CreatedAt = DateTime.UtcNow
        //     };
        //
        //     var newEmployee = new Employee 
        //     { 
        //         FirstName = "Jane", 
        //         LastName = "Smith", 
        //         Email = "jane.smith@email.com",
        //         DocumentNumber = "11223344556",
        //         Role = RoleType.Leader, 
        //         PasswordHash = "hashedpassword123",
        //         DateOfBirth = new DateTime(1995, 5, 15),
        //         CreatedAt = DateTime.UtcNow
        //     };
        //
        //     // Act & Assert
        //     var exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(
        //         () => _employeeService.AddEmployeeAsync(newEmployee));
        //
        //     Assert.Equal("Usuário não tem permissão para criar esse tipo de funcionário.", exception.Message);
        // }
    }