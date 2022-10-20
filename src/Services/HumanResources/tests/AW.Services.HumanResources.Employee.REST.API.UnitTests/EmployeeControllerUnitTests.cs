using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.Exceptions;
using AW.Services.HumanResources.Core.Handlers.GetEmployee;
using AW.Services.HumanResources.Core.Handlers.GetEmployees;
using AW.Services.HumanResources.Employee.REST.API.Controllers;
using AW.SharedKernel.UnitTesting;
using AW.SharedKernel.ValueTypes;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AW.Services.HumanResources.Employee.REST.API.UnitTests
{
    public class EmployeeControllerUnitTests
    {
        public class GetEmployees
        {
            [Theory, AutoMoqData]
            public async Task GetEmployees_EmployeesExists_ShouldReturnEmployees(

                [Frozen] Mock<IMediator> mockMediator,
                List<Core.Handlers.GetEmployees.Employee> employees,
                [Greedy] EmployeeController sut,
                GetEmployeesQuery query
            )
            {
                //Arrange
                var result = new GetEmployeesResult(
                    employees: employees,
                    totalEmployees: employees.Count
                );

                mockMediator.Setup(x => x.Send(
                        It.IsAny<GetEmployeesQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(result);

                //Act
                var actionResult = await sut.GetEmployees(query);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var response = okObjectResult?.Value as GetEmployeesResult;
                response?.TotalEmployees.Should().Be(employees.Count);
                response?.Employees.Count.Should().Be(employees.Count);
                response?.Employees.Should().BeEquivalentTo(employees);
            }

            [Theory]
            [AutoMoqData]
            public async Task GetEmployees_NoEmployees_ShouldReturnNotFound(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] EmployeeController sut,
                GetEmployeesQuery query
            )
            {
                //Arrange                
                mockMediator.Setup(x => x.Send(
                    It.IsAny<GetEmployeesQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .Throws<EmployeesNotFoundException>();

                //Act
                var actionResult = await sut.GetEmployees(query);

                //Assert
                var notFoundResult = actionResult as NotFoundResult;
                notFoundResult.Should().NotBeNull();
            }
        }

        public class GetEmployee
        {
            [Theory]
            [AutoMoqData]
            public async Task GetEmployee_EmployeeExists_ShouldReturnEmployee(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] EmployeeController sut,
                GetEmployeeQuery query,
                NameFactory name,
                List<Core.Handlers.GetEmployee.EmployeeDepartmentHistory> history
            )
            {
                //Arrange
                var employee = new Core.Handlers.GetEmployee.Employee(
                    name,
                    history
                );

                mockMediator.Setup(x => x.Send(
                    It.IsAny<GetEmployeeQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(employee);

                //Act
                var actionResult = await sut.GetEmployee(query);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var result = okObjectResult?.Value as Core.Handlers.GetEmployee.Employee;
                result.Should().NotBeNull();
            }

            [Theory]
            [AutoMoqData]
            public async Task GetEmployee_EmployeeDoesNotExists_ShouldReturnNotFound(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] EmployeeController sut,
                GetEmployeeQuery query
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                    It.IsAny<GetEmployeeQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .Throws(new EmployeeNotFoundException(query.LoginID));

                //Act
                var actionResult = await sut.GetEmployee(query);

                //Assert
                var notFoundResult = actionResult as NotFoundResult;
                notFoundResult.Should().NotBeNull();
            }
        }
    }
}