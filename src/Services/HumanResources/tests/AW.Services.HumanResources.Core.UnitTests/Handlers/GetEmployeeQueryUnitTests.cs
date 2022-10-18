using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.AutoMapper;
using AW.Services.HumanResources.Core.Exceptions;
using AW.Services.HumanResources.Core.Handlers.GetEmployee;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class GetEmployeeQueryUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_EmployeeExists_ReturnEmployee(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepoMock,
            GetEmployeeQueryHandler sut,
            GetEmployeeQuery query,
            string loginID
        )
        {
            //Arrange
            var employee = new Entities.Employee
            {
                LoginID = loginID,
                MaritalStatus = Entities.MaritalStatus.Married
            };

            employeeRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(employee);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            employeeRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            result.Should().BeEquivalentTo(employee, opt => opt
                .Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
            );
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_EmployeeNotFound_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepoMock,
            GetEmployeeQueryHandler sut,
            GetEmployeeQuery query
        )
        {
            // Arrange
            employeeRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.Employee?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<EmployeeNotFoundException>()
                .WithMessage($"Employee {query.LoginID} not found");
        }
    }
}