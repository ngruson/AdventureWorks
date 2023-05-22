using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.Handlers.UpdateDepartmentHistory;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class UpdateDepartmentHistoryCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task return_success_given_department_history_was_updated(
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepoMock,
            [Frozen] Mock<IRepository<Entities.Shift>> shiftRepoMock,
            Entities.Employee employee,
            Entities.EmployeeDepartmentHistory departmentHistory,
            Entities.Department department,
            Entities.Shift shift,
            UpdateDepartmentHistoryCommandHandler sut
        )
        {
            //Arrange
            employee.DepartmentHistory.Add(departmentHistory);

            employeeRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetEmployeeSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            ).
            ReturnsAsync(employee);

            departmentRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetDepartmentSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            ).
            ReturnsAsync(department);

            shiftRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetShiftSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            ).
            ReturnsAsync(shift);

            //Act
            var command = new UpdateDepartmentHistoryCommand
            {
                ObjectId = departmentHistory.ObjectId,
                Employee = employee.ObjectId,
                Department = departmentHistory.Department!.ObjectId,
                Shift = departmentHistory.Shift!.ObjectId,
                StartDate = departmentHistory.StartDate
            };

            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();

            employeeRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
            ));
            departmentRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetDepartmentSpecification>(),
                It.IsAny<CancellationToken>()
            ));
            employeeRepoMock.Verify(x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory]
        [AutoMoqData]
        public async Task return_notfound_given_employee_does_not_exist(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepoMock,
            UpdateDepartmentHistoryCommandHandler sut,
            UpdateDepartmentHistoryCommand command
        )
        {
            // Arrange
            employeeRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.Employee?)null);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.NotFound);
            result.Errors.Should().Contain($"Employee {command.Employee} not found");

            employeeRepoMock.Verify(_ => _.SingleOrDefaultAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            employeeRepoMock.Verify(x => x.SaveChangesAsync(
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory]
        [AutoMoqData]
        public async Task return_notfound_given_department_does_not_exist(
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepoMock,
            UpdateDepartmentHistoryCommandHandler sut, 
            UpdateDepartmentHistoryCommand command,
            Entities.Employee employee
        )
        {
            // Arrange
            employeeRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetEmployeeSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            ).
            ReturnsAsync(employee);

            departmentRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetDepartmentSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.Department?)null);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.NotFound);
            result.Errors.Should().Contain($"Department '{command.Department}' not found");

            employeeRepoMock.Verify(_ => _.SingleOrDefaultAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            departmentRepoMock.Verify(_ => _.SingleOrDefaultAsync(
                It.IsAny<GetDepartmentSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            employeeRepoMock.Verify(x => x.SaveChangesAsync(
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory]
        [AutoMoqData]
        public async Task return_notfound_given_shift_does_not_exist(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepoMock,
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            [Frozen] Mock<IRepository<Entities.Shift>> shiftRepoMock,
            UpdateDepartmentHistoryCommandHandler sut,
            UpdateDepartmentHistoryCommand command,
            Entities.Employee employee,
            Entities.Department department
        )
        {
            // Arrange
            employeeRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetEmployeeSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            ).
            ReturnsAsync(employee);

            departmentRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetDepartmentSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(department);

            shiftRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetShiftSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.Shift?)null);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.NotFound);
            result.Errors.Should().Contain($"Shift '{command.Shift}' not found");

            employeeRepoMock.Verify(_ => _.SingleOrDefaultAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            departmentRepoMock.Verify(_ => _.SingleOrDefaultAsync(
                It.IsAny<GetDepartmentSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            shiftRepoMock.Verify(_ => _.SingleOrDefaultAsync(
                It.IsAny<GetShiftSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            employeeRepoMock.Verify(x => x.SaveChangesAsync(
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }
    }
}
