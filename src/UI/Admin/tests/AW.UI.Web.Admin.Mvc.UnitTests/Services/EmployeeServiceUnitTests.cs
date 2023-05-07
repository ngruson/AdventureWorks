using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Employee;
using AW.UI.Web.Infrastructure.Api.Department.Handlers.GetDepartments;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.AddDepartmentHistory;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.DeleteDepartmentHistory;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.GetEmployee;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.GetEmployees;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.GetJobTitles;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.UpdateDepartmentHistory;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.UpdateEmployee;
using AW.UI.Web.Infrastructure.Api.Shift.Handlers.GetShifts;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace AW.UI.Web.Admin.Mvc.UnitTests.Services
{
    public class EmployeeServiceUnitTests
    {
        public class GetEmployees
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ReturnsViewModel(
                [Frozen] Mock<IMediator> mockMediator,
                EmployeeService sut,
                List<Infrastructure.Api.Employee.Handlers.GetEmployees.Employee> employees
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                        It.IsAny<GetEmployeesQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(employees);

                //Act
                var viewModel = await sut.GetEmployees();

                //Assert
                viewModel.Should().BeEquivalentTo(employees);
            }
        }

        public class GetDetail
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ReturnsDetailViewModel(
                [Frozen] Mock<IMediator> mockMediator,
                EmployeeService sut,
                Infrastructure.Api.Employee.Handlers.GetEmployee.Employee employee,
                string loginID
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                        It.IsAny<GetEmployeeQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(employee);

                //Act
                var actual = await sut.GetDetail(loginID);

                //Assert
                actual.Employee.Should().BeEquivalentTo(employee);

                mockMediator.Verify(_ => _.Send(
                        It.IsAny<GetEmployeeQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                );
            }
        }

        public class GetDepartments
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ReturnDepartments(
                [Frozen] Mock<IMediator> mockMediator,
                EmployeeService sut,
                List<Infrastructure.Api.Department.Handlers.GetDepartments.Department> departments
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                        It.IsAny<GetDepartmentsQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(departments);

                //Act
                var actual = await sut.GetDepartments();

                //Assert
                actual.Should().BeEquivalentTo(departments);

                mockMediator.Verify(_ => _.Send(
                        It.IsAny<GetDepartmentsQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                );
            }
        }

        public class GetShifts
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ReturnShifts(
                [Frozen] Mock<IMediator> mockMediator,
                EmployeeService sut,
                List<Infrastructure.Api.Shift.Handlers.GetShifts.Shift> shifts
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                        It.IsAny<GetShiftsQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(shifts);

                //Act
                var actual = await sut.GetShifts();

                //Assert
                actual.Should().BeEquivalentTo(shifts);

                mockMediator.Verify(_ => _.Send(
                        It.IsAny<GetShiftsQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                );
            }
        }

        public class GetJobTitles
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ReturnJobTitles(
                [Frozen] Mock<IMediator> mockMediator,
                EmployeeService sut,
                List<string> jobTitles
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                        It.IsAny<GetJobTitlesQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(jobTitles);

                //Act
                var actual = await sut.GetJobTitles();

                //Assert
                actual.Should().BeEquivalentTo(jobTitles);

                mockMediator.Verify(_ => _.Send(
                        It.IsAny<GetJobTitlesQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                );
            }
        }

        public class UpdateEmployee
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ExecuteCommand(
                [Frozen] Mock<IMediator> mockMediator,
                EmployeeService sut,
                EditEmployeeViewModel viewModel
            )
            {
                //Arrange

                //Act
                await sut.UpdateEmployee(viewModel);

                //Assert
                mockMediator.Verify(_ => _.Send(
                        It.IsAny<UpdateEmployeeCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                );
            }
        }

        public class AddDepartmentHistory
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ExecuteCommand(
                [Frozen] Mock<IMediator> mockMediator,
                EmployeeService sut,
                AddDepartmentHistoryViewModel viewModel
            )
            {
                //Arrange

                //Act
                await sut.AddDepartmentHistory(viewModel);

                //Assert
                mockMediator.Verify(_ => _.Send(
                        It.IsAny<AddDepartmentHistoryCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                );
            }
        }

        public class UpdateDepartmentHistory
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ExecuteCommand(
                [Frozen] Mock<IMediator> mockMediator,
                EmployeeService sut,
                UpdateDepartmentHistoryViewModel viewModel
            )
            {
                //Arrange

                //Act
                await sut.UpdateDepartmentHistory(viewModel);

                //Assert
                mockMediator.Verify(_ => _.Send(
                        It.IsAny<UpdateDepartmentHistoryCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                );
            }
        }

        public class DeleteDepartmentHistory
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ExecuteCommand(
                [Frozen] Mock<IMediator> mockMediator,
                EmployeeService sut,
                string loginID,
                string departmentName,
                string shiftName,
                DateTime startDate
            )
            {
                //Arrange

                //Act
                await sut.DeleteDepartmentHistory(
                    loginID,
                    departmentName,
                    shiftName,
                    startDate
                );

                //Assert
                mockMediator.Verify(_ => _.Send(
                        It.IsAny<DeleteDepartmentHistoryCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                );
            }
        }
    }
}
