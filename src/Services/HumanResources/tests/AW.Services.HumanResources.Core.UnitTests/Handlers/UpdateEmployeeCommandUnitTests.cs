using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.AutoMapper;
using AW.Services.HumanResources.Core.Exceptions;
using AW.Services.HumanResources.Core.Handlers.UpdateEmployee;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class UpdateEmployeeCommandUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task ReturnUpdatedEmployeeGivenEmployeeExists(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepoMock,
            Entities.Employee employee,
            UpdateEmployeeCommandHandler sut,
            UpdateEmployeeCommand command
        )
        {
            //Arrange
            command.Employee!.MaritalStatus = Entities.MaritalStatus.Married.Name;
            command.Employee!.Gender = Entities.Gender.Male.Name;

            employeeRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetEmployeeSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            ).
            ReturnsAsync(employee);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            employeeRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
            ));
            employeeRepoMock.Verify(x => x.UpdateAsync(
                It.IsAny<Entities.Employee>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory]
        [AutoMoqData]
        public async Task ThrowEmployeeNotFoundExceptionGivenEmployeeDoesNotExist(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepoMock,
            UpdateEmployeeCommandHandler sut,
            UpdateEmployeeCommand command
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
                .WithMessage($"Employee {command.Key} not found");
        }
    }
}
