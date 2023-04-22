using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Employee.Handlers.GetJobTitles;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Employee.Handlers
{
    public class GetJobTitlesQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task ReturnJobTitlesGivenJobTitlesExist(
            [Frozen] Mock<IEmployeeApiClient> mockEmployeeApiClient,
            GetJobTitlesQueryHandler sut,
            GetJobTitlesQuery query,
            List<string> jobTitles
        )
        {
            //Arrange
            mockEmployeeApiClient.Setup(_ => _.GetJobTitles())
                .ReturnsAsync(jobTitles);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().BeEquivalentTo(jobTitles);

            mockEmployeeApiClient.Verify(_ => _.GetJobTitles());
        }

        [Theory, AutoMoqData]
        public async Task ThrowArgumentNullExceptionGivenJobTitlesAreNull(
            [Frozen] Mock<IEmployeeApiClient> mockEmployeeApiClient,
            GetJobTitlesQueryHandler sut,
            GetJobTitlesQuery query
        )
        {
            //Arrange

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            mockEmployeeApiClient.Verify(_ => _.GetJobTitles());
        }
    }
}
