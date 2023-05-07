using AutoFixture.Xunit2;
using AW.SharedKernel.Caching;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetCountries;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.ReferenceData.Handlers
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
