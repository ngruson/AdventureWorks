using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Department.Handlers.GetDepartment;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Department.Handlers
{
    public class GetDepartmentQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task ReturnDepartmentGivenDepartmentExist(
            [Frozen] Mock<IDepartmentApiClient> mockDepartmentApiClient,
            GetDepartmentQueryHandler sut,
            GetDepartmentQuery query,
            Infrastructure.Api.Department.Handlers.GetDepartment.Department department
        )
        {
            //Arrange
            mockDepartmentApiClient.Setup(_ => _.GetDepartment(query.Name))
                .ReturnsAsync(department);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().Be(department);
            mockDepartmentApiClient.Verify(_ => _.GetDepartment(query.Name));
        }

        [Theory, AutoMoqData]
        public async Task ThrowArgumentNullExceptionGivenDepartmentsAreNull(
            [Frozen] Mock<IDepartmentApiClient> mockDepartmentApiClient,
            GetDepartmentQueryHandler sut,
            GetDepartmentQuery query
        )
        {
            //Arrange
            mockDepartmentApiClient.Setup(_ => _.GetDepartment(query.Name))
                .ReturnsAsync((Infrastructure.Api.Department.Handlers.GetDepartment.Department?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            mockDepartmentApiClient.Verify(_ => _.GetDepartment(query.Name));
        }
    }
}
