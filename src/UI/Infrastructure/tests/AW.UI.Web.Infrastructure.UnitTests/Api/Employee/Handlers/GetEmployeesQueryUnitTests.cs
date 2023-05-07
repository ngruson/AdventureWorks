using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.GetEmployees;
using Xunit;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using System.Collections.Generic;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Employee.Handlers
{
    public class GetEmployeesQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task ReturnEmployeesGivenEmployeesExist(
            [Frozen] Mock<IEmployeeApiClient> mockEmployeeApiClient,
            GetEmployeesQueryHandler sut,
            GetEmployeesQuery query,
            List<Infrastructure.Api.Employee.Handlers.GetEmployees.Employee> employees
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
            mockEmployeeApiClient.Setup(_ => _.GetEmployees())
                .ReturnsAsync((List<Infrastructure.Api.Employee.Handlers.GetEmployees.Employee>?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            mockEmployeeApiClient.Verify(_ => _.GetEmployees());
        }
    }
}
