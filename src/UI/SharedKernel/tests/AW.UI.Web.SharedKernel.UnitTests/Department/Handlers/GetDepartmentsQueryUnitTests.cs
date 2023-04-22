using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Department.Handlers.GetDepartments;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Department.Handlers
{
    public class GetDepartmentsQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task ReturnDepartmentsGivenDepartmentsExist(
            [Frozen] Mock<IDepartmentApiClient> mockDepartmentApiClient,
            GetDepartmentsQueryHandler sut,
            GetDepartmentsQuery query,
            List<SharedKernel.Department.Handlers.GetDepartments.Department> departments
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

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            mockDepartmentApiClient.Verify(_ => _.GetDepartments());
        }
    }
}
