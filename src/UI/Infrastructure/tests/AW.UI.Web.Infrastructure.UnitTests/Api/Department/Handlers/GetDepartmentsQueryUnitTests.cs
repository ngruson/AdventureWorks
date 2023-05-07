using System.Collections.Generic;
using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Department.Handlers.GetDepartments;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Department.Handlers
{
    public class GetDepartmentsQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task ReturnDepartmentsGivenDepartmentsExist(
            [Frozen] Mock<IDepartmentApiClient> mockDepartmentApiClient,
            GetDepartmentsQueryHandler sut,
            GetDepartmentsQuery query,
            List<Infrastructure.Api.Department.Handlers.GetDepartments.Department> departments
        )
        {
            //Arrange
            mockDepartmentApiClient.Setup(_ => _.GetDepartments())
                .ReturnsAsync(departments);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().BeEquivalentTo(departments);

            mockDepartmentApiClient.Verify(_ => _.GetDepartments());
        }

        [Theory, AutoMoqData]
        public async Task ThrowArgumentNullExceptionGivenDepartmentsAreNull(
            [Frozen] Mock<IDepartmentApiClient> mockDepartmentApiClient,
            GetDepartmentsQueryHandler sut,
            GetDepartmentsQuery query
        )
        {
            //Arrange
            mockDepartmentApiClient.Setup(_ => _.GetDepartments())
                .ReturnsAsync((List<Infrastructure.Api.Department.Handlers.GetDepartments.Department>?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            mockDepartmentApiClient.Verify(_ => _.GetDepartments());
        }
    }
}
