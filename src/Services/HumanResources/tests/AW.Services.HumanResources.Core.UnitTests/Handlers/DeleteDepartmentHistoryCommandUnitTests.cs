using Ardalis.Result;
using AutoFixture.Xunit2;
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
        public async Task return_success_given_departmenthistory_was_deleted(
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
            var command = new DeleteDepartmentHistoryCommand(
                employee.ObjectId,
                edh.ObjectId
            );
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();

            employeeRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
            ));
            employeeRepoMock.Verify(x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task return_notfound_given_employee_does_not_exist(
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
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.NotFound);
            result.Errors.Should().Contain($"Employee {command.Employee} not found");

            employeeRepoMock.Verify(_ => _.SingleOrDefaultAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            employeeRepoMock.Verify(_ => _.SaveChangesAsync(
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory, AutoMoqData]
        public async Task return_notfound_given_deparmenthistory_not_found(
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
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.NotFound);
            result.Errors.Should().Contain($"Department history {command.ObjectId} not found");

            employeeRepoMock.Verify(_ => _.SingleOrDefaultAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            employeeRepoMock.Verify(_ => _.SaveChangesAsync(
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }
    }
}
