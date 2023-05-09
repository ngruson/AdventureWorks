using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.AutoMapper;
using AW.Services.HumanResources.Core.Exceptions;
using AW.Services.HumanResources.Core.Handlers.AddDepartmentHistory;
using AW.Services.HumanResources.Core.Handlers.DeleteDepartmentHistory;
using AW.Services.HumanResources.Core.Handlers.GetEmployee;
using AW.Services.HumanResources.Core.Handlers.GetEmployees;
using AW.Services.HumanResources.Core.Handlers.UpdateDepartmentHistory;
using AW.Services.HumanResources.Core.Handlers.UpdateEmployee;
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
                mockMediator.Setup(x => x.Send(
                        It.IsAny<GetEmployeesQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(employees);

                //Act
                var actionResult = await sut.GetEmployees(query);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var response = okObjectResult?.Value as List<Core.Handlers.GetEmployees.Employee>;
                response.Should().BeEquivalentTo(employees);
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
                .Throws(new EmployeeNotFoundException(query.LoginID!));

                //Act
                var actionResult = await sut.GetEmployee(query);

                //Assert
                var notFoundResult = actionResult as NotFoundResult;
                notFoundResult.Should().NotBeNull();
            }
        }

        public class UpdateEmployee
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ReturnEmployeeWhenEmployeeExists(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] EmployeeController sut,
                Core.Handlers.UpdateEmployee.Employee employee
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                    It.IsAny<UpdateEmployeeCommand>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(employee);

                //Act
                var actionResult = await sut.UpdateEmployee(employee.LoginID!, employee);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var response = okObjectResult?.Value as Core.Handlers.UpdateEmployee.Employee;
                response?.LoginID.Should().Be(employee.LoginID);
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ReturnNotFoundWhenEmployeeDoesNotExist(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] EmployeeController sut,
                Core.Handlers.UpdateEmployee.Employee employee
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                        It.IsAny<UpdateEmployeeCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ThrowsAsync(new EmployeeNotFoundException(employee.LoginID!));

                //Act
                var actionResult = await sut.UpdateEmployee(employee.LoginID!, employee);

                //Assert
                actionResult.Should().BeOfType<NotFoundResult>();
            }
        }

        public class AddDepartmentHistory
        {
            [Theory, AutoMoqData]
            public async Task ReturnOkWhenSuccess(
                [Greedy] EmployeeController sut,
                AddDepartmentHistoryCommand command
            )
            {
                //Arrange

                //Act
                var actionResult = await sut.AddDepartmentHistory(command);

                //Assert
                actionResult.Should().BeOfType<OkResult>();
            }

            [Theory, AutoMoqData]
            public async Task ReturnNotFoundWhenEmployeeDoesNotExist(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] EmployeeController sut,
                AddDepartmentHistoryCommand command
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                        It.IsAny<AddDepartmentHistoryCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .Throws(new EmployeeNotFoundException(command.LoginID!));

                //Act
                var actionResult = await sut.AddDepartmentHistory(command);

                //Assert
                actionResult.Should().BeOfType<NotFoundResult>();
            }

            [Theory, AutoMoqData]
            public async Task ReturnNotFoundWhenDepartmentDoesNotExist(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] EmployeeController sut,
                AddDepartmentHistoryCommand command
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                        It.IsAny<AddDepartmentHistoryCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .Throws(new DepartmentNotFoundException(command.Department!));

                //Act
                var actionResult = await sut.AddDepartmentHistory(command);

                //Assert
                actionResult.Should().BeOfType<NotFoundResult>();
            }

            [Theory, AutoMoqData]
            public async Task ReturnNotFoundWhenShiftDoesNotExist(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] EmployeeController sut,
                AddDepartmentHistoryCommand command
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                        It.IsAny<AddDepartmentHistoryCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .Throws(new ShiftNotFoundException(command.Shift));

                //Act
                var actionResult = await sut.AddDepartmentHistory(command);

                //Assert
                actionResult.Should().BeOfType<NotFoundResult>();
            }
        }

        public class UpdateDepartmentHistory
        {
            [Theory, AutoMoqData]
            public async Task ReturnOkWhenSuccess(
                [Greedy] EmployeeController sut,
                UpdateDepartmentHistoryCommand command
            )
            {
                //Arrange

                //Act
                var actionResult = await sut.UpdateDepartmentHistory(command);

                //Assert
                actionResult.Should().BeOfType<OkResult>();
            }

            [Theory, AutoMoqData]
            public async Task ReturnNotFoundWhenEmployeeDoesNotExist(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] EmployeeController sut,
                UpdateDepartmentHistoryCommand command
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                        It.IsAny<UpdateDepartmentHistoryCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .Throws(new EmployeeNotFoundException(command.LoginID!));

                //Act
                var actionResult = await sut.UpdateDepartmentHistory(command);

                //Assert
                actionResult.Should().BeOfType<NotFoundResult>();
            }

            [Theory, AutoMoqData]
            public async Task ReturnNotFoundWhenDepartmentDoesNotExist(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] EmployeeController sut,
                UpdateDepartmentHistoryCommand command
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                        It.IsAny<UpdateDepartmentHistoryCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .Throws(new DepartmentNotFoundException(command.Department));

                //Act
                var actionResult = await sut.UpdateDepartmentHistory(command);

                //Assert
                actionResult.Should().BeOfType<NotFoundResult>();
            }

            [Theory, AutoMoqData]
            public async Task ReturnNotFoundWhenShiftDoesNotExist(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] EmployeeController sut,
                UpdateDepartmentHistoryCommand command
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                        It.IsAny<UpdateDepartmentHistoryCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .Throws(new ShiftNotFoundException(command.ObjectId));

                //Act
                var actionResult = await sut.UpdateDepartmentHistory(command);

                //Assert
                actionResult.Should().BeOfType<NotFoundResult>();
            }
        }

        public class DeleteDepartmentHistory
        {
            [Theory, AutoMoqData]
            public async Task ReturnOkWhenSuccess(
                [Greedy] EmployeeController sut,
                DeleteDepartmentHistoryCommand command
            )
            {
                //Arrange

                //Act
                var actionResult = await sut.DeleteDepartmentHistory(command);

                //Assert
                actionResult.Should().BeOfType<OkResult>();
            }

            [Theory, AutoMoqData]
            public async Task ReturnNotFoundWhenEmployeeDoesNotExist(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] EmployeeController sut,
                DeleteDepartmentHistoryCommand command
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                        It.IsAny<DeleteDepartmentHistoryCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .Throws(new EmployeeNotFoundException(command.LoginID!));

                //Act
                var actionResult = await sut.DeleteDepartmentHistory(command);

                //Assert
                actionResult.Should().BeOfType<NotFoundResult>();
            }

            [Theory, AutoMoqData]
            public async Task ReturnNotFoundWhenDepartmentHistoryDoesNotExist(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] EmployeeController sut,
                DeleteDepartmentHistoryCommand command
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                        It.IsAny<DeleteDepartmentHistoryCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .Throws(new EmployeeDepartmentHistoryNotFoundException(
                    command.ObjectId
                ));

                //Act
                var actionResult = await sut.DeleteDepartmentHistory(command);

                //Assert
                actionResult.Should().BeOfType<NotFoundResult>();
            }
        }
    }
}
