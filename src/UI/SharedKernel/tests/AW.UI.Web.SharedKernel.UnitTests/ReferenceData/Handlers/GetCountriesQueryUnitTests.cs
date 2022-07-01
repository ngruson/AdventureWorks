using AutoFixture.Xunit2;
using AW.SharedKernel.Caching;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetCountries;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.ReferenceData.Handlers
{
    public class GetCountriesQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_ReturnCountries(
            List<CountryRegion> countries,
            [Frozen] Mock<ICache<CountryRegion>> cacheMock,
            GetCountriesQueryHandler sut,
            GetCountriesQuery query
        )
        {
            //Arrange
            cacheMock.Setup(x => x.GetData())
                .ReturnsAsync(countries);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            cacheMock.Verify(x => x.GetData());
            result.Should().BeEquivalentTo(countries);
        }
    }
}