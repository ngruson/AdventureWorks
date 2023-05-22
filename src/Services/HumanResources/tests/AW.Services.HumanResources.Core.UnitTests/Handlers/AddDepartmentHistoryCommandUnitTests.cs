using Ardalis.Result;
using AutoFixture.Xunit2;
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
        public async Task return_success_given_departmenthistory_was_added(
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
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.NotFound);
            result.Errors.Should().Contain($"Employee {command.Employee} not found");

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
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.NotFound);
            result.Errors.Should().Contain($"Department '{command.Department}' not found");

            employeeRepoMock.Verify(x => x.SaveChangesAsync(
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory]
        [AutoMoqData]
        public async Task return_notfound_given_shift_does_not_exist(
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepoMock,
            [Frozen] Mock<IRepository<Entities.Shift>> shiftRepoMock,
            AddDepartmentHistoryCommandHandler sut,
            AddDepartmentHistoryCommand command,
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

            employeeRepoMock.Verify(x => x.SaveChangesAsync(
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory, AutoMoqData]
        public async Task return_error_given_exception_was_thrown(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepoMock,
            AddDepartmentHistoryCommandHandler sut,
            AddDepartmentHistoryCommand command
        )
        {
            //Arrange

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            //result.Status.Should().Be(ResultStatus.Error); //TODO

            employeeRepoMock.Verify(x => x.SaveChangesAsync(
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }
    }
}
