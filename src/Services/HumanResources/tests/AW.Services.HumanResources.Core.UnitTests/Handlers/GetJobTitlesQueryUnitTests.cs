using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.Handlers.GetJobTitles;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class GetJobTitlesQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task ReturnJobTitles(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepoMock,
            GetJobTitlesQueryHandler sut,
            GetJobTitlesQuery query,
            List<string> jobTitles
        )
        {
            // Arrange
            employeeRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetJobTitlesSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(jobTitles);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert            
            result.Value.Should().BeEquivalentTo(jobTitles);

            employeeRepoMock.Verify(x => x.ListAsync(
                It.IsAny<GetJobTitlesSpecification>(),
                It.IsAny<CancellationToken>()
            ));
        }
    }
}
