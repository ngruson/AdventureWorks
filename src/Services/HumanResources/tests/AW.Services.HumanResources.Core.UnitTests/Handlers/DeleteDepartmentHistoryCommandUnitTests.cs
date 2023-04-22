using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.Exceptions;
using AW.Services.HumanResources.Core.Handlers.DeleteDepartmentHistory;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class DeleteDepartmentHistoryCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task DeleteDepartmentHistoryGivenEmployeeAndDepartmentHistoryExist(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepoMock,
            Entities.Employee employee,
            Entities.EmployeeDepartmentHistory edh,
            DeleteDepartmentHistoryCommandHandler sut
        )
        {
            //Arrange
            employee.DepartmentHistory.Add(edh);

            employeeRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetEmployeeSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            ).
            ReturnsAsync(employee);

            //Act
            var command = new DeleteDepartmentHistoryCommand
            {
                LoginID = employee.LoginID,
                DepartmentName = edh.Department!.Name,
                ShiftName = edh.Shift!.Name,
                StartDate = edh.StartDate
            };
            await sut.Handle(command, CancellationToken.None);

            //Assert
            employeeRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
            ));
            employeeRepoMock.Verify(x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task ThrowEmployeeNotFoundExceptionGivenEmployeeDoesNotExist(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepoMock,
            DeleteDepartmentHistoryCommandHandler sut,
            DeleteDepartmentHistoryCommand command
        )
        {
            //Arrange
            employeeRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.Employee?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<EmployeeNotFoundException>()
                .WithMessage($"Employee {command.LoginID} not found");
        }

        [Theory, AutoMoqData]
        public async Task ThrowEmployeeDepartmentHistoryNotFoundExceptionGivenDepartmentHistoryDoesNotExist(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepoMock,
            Entities.Employee employee,
            DeleteDepartmentHistoryCommandHandler sut,
            DeleteDepartmentHistoryCommand command
        )
        {
            //Arrange
            employeeRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetEmployeeSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            ).
            ReturnsAsync(employee);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<EmployeeDepartmentHistoryNotFoundException>()
                .WithMessage($"Department history not found for employee {command.LoginID}, " +
                    $"department {command.DepartmentName}, " +
                    $"shift {command.ShiftName}, " +
                    $"start date {command.StartDate.ToShortDateString()}"
                );
        }
    }
}
