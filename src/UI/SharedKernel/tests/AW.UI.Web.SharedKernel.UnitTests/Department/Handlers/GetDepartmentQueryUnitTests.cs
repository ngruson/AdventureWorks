using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Department.Handlers.GetDepartment;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Department.Handlers
{
    public class GetDepartmentQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task ReturnDepartmentGivenDepartmentExist(
            [Frozen] Mock<IDepartmentApiClient> mockDepartmentApiClient,
            GetDepartmentQueryHandler sut,
            GetDepartmentQuery query,
            SharedKernel.Department.Handlers.GetDepartment.Department department
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

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            mockDepartmentApiClient.Verify(_ => _.GetDepartment(query.Name));
        }
    }
}
