using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.Employee.Handlers.GetEmployee;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Employee.Handlers
{
    public class GetEmployeeQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task ReturnEmployeeGivenEmployeeExistsf(
            [Frozen] Mock<IEmployeeApiClient> mockEmployeeApiClient,
            GetEmployeeQueryHandler sut,
            GetEmployeeQuery query,
            SharedKernel.Employee.Handlers.GetEmployee.Employee employee
        )
        {
            //Arrange
            mockEmployeeApiClient.Setup(_ => _.GetEmployee(
                    It.IsAny<string>()
                )
            )
            .ReturnsAsync(employee);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().Be(employee);

            mockEmployeeApiClient.Verify(_ => _.GetEmployee(
                    It.IsAny<string>()
                )
            );
        }

        [Theory, AutoMoqData]
        public async Task ThrowArgumentNullExceptionGivenEmployeeIsNull(
            [Frozen] Mock<IEmployeeApiClient> mockEmployeeApiClient,
            GetEmployeeQueryHandler sut,
            GetEmployeeQuery query
        )
        {
            //Arrange

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            mockEmployeeApiClient.Verify(_ => _.GetEmployee(
                    It.IsAny<string>()
                )
            );
        }
    }
}
