using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.Employee.Handlers.GetEmployees;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Employee.Handlers
{
    public class GetEmployeesQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task ReturnEmployeesGivenEmployeesExist(
            [Frozen] Mock<IEmployeeApiClient> mockEmployeeApiClient,
            GetEmployeesQueryHandler sut,
            GetEmployeesQuery query,
            List<SharedKernel.Employee.Handlers.GetEmployees.Employee> employees
        )
        {
            //Arrange
            mockEmployeeApiClient.Setup(_ => _.GetEmployees())
                .ReturnsAsync(employees);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().BeEquivalentTo(employees);

            mockEmployeeApiClient.Verify(_ => _.GetEmployees());
        }

        [Theory, AutoMoqData]
        public async Task ThrowArgumentNullExceptionGivenEmployeesAreNull(
            [Frozen] Mock<IEmployeeApiClient> mockEmployeeApiClient,
            GetEmployeesQueryHandler sut,
            GetEmployeesQuery query
        )
        {
            //Arrange

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            mockEmployeeApiClient.Verify(_ => _.GetEmployees());
        }
    }
}
