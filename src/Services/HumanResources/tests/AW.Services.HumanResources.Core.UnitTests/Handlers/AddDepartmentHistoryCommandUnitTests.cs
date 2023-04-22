using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.Exceptions;
using AW.Services.HumanResources.Core.Handlers.AddDepartmentHistory;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class AddDepartmentHistoryCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task AddDepartmentHistoryGivenEmployeeAndDepartmentExist(
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepoMock,
            [Frozen] Mock<IRepository<Entities.Shift>> shiftRepoMock,
            Entities.Employee employee,
            Entities.Department department,
            Entities.Shift shift,
            AddDepartmentHistoryCommandHandler sut,
            AddDepartmentHistoryCommand command
        )
        {
            //Arrange
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
            await sut.Handle(command, CancellationToken.None);

            //Assert
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
        public async Task ThrowEmployeeNotFoundExceptionGivenEmployeeDoesNotExist(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepoMock,
            AddDepartmentHistoryCommandHandler sut,
            AddDepartmentHistoryCommand command
        )
        {
            // Arrange
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

        [Theory]
        [AutoMoqData]
        public async Task ThrowDepartmentNotFoundExceptionGivenDepartmentDoesNotExist(
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepoMock,
            AddDepartmentHistoryCommandHandler sut,
            AddDepartmentHistoryCommand command,
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
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<DepartmentNotFoundException>()
                .WithMessage($"Department '{command.DepartmentName}' not found");
        }
    }
}
