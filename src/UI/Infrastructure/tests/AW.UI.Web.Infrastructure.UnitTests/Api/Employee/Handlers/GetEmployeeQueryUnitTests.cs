using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.GetEmployee;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Employee.Handlers
{
    public class GetEmployeeQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task ReturnEmployeeGivenEmployeeExistsf(
            [Frozen] Mock<IEmployeeApiClient> mockEmployeeApiClient,
            GetEmployeeQueryHandler sut,
            GetEmployeeQuery query,
            Infrastructure.Api.Employee.Handlers.GetEmployee.Employee employee
        )
        {
            //Arrange
            mockEmployeeApiClient.Setup(_ => _.GetEmployee(
                    query.ObjectId
                )
            )
            .ReturnsAsync(employee);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().Be(employee);

            mockEmployeeApiClient.Verify(_ => _.GetEmployee(
                    query.ObjectId
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
            mockEmployeeApiClient.Setup(_ => _.GetEmployee(
                    query.ObjectId
                )
            )
            .ReturnsAsync((Infrastructure.Api.Employee.Handlers.GetEmployee.Employee?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            mockEmployeeApiClient.Verify(_ => _.GetEmployee(
                    query.ObjectId
                )
            );
        }
    }
}
