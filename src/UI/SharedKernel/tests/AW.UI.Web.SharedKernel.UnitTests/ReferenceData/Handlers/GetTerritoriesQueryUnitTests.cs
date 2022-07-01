using AutoFixture.Xunit2;
using AW.SharedKernel.Caching;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetTerritories;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.ReferenceData.Handlers
{
    public class GetTerritoriesQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_WithoutCountryRegionCode_ReturnTerritories(
            List<Territory> territories,
            [Frozen] Mock<ICache<Territory>> cacheMock,
            GetTerritoriesQueryHandler sut
        )
        {
            //Arrange
            var query = new GetTerritoriesQuery();

            cacheMock.Setup(_ => _.GetData())
                .ReturnsAsync(territories);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            cacheMock.Verify(_ => _.GetData());
            result.Should().BeEquivalentTo(territories);
        }

        [Theory, AutoMoqData]
        public async Task Handle_WithCountryRegionCode_ReturnTerritories(
            List<Territory> territories,
            [Frozen] Mock<ICache<Territory>> cacheMock,
            GetTerritoriesQueryHandler sut,
            GetTerritoriesQuery query
        )
        {
            //Arrange
            cacheMock.Setup(x => x.GetData(
                    It.IsAny<Func<Territory, bool>>()
                )
            )
            .ReturnsAsync(territories);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            cacheMock.Verify(x => x.GetData(
                    It.IsAny<Func<Territory, bool>>()
                )
            );
            result.Should().BeEquivalentTo(territories);
        }
    }
}