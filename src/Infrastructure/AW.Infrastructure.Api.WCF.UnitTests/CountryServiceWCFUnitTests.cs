using AutoMapper;
using AW.Infrastructure.Api.WCF.AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AW.Infrastructure.Api.WCF.UnitTests
{
    public class CountryServiceWCFUnitTests
    {
        [Fact]
        public async void ListCountries_ReturnsCountries()
        {
            //Arrange
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<CountryProfile>())
                .CreateMapper();
            var mockLogger = new Mock<ILogger<CountryServiceWCF>>();
            var mockCountryService = new Mock<CountryService.ICountryService>();
            mockCountryService.Setup(x => x.ListCountriesAsync())
                .ReturnsAsync(new CountryService.ListCountriesResponse
                {
                    Countries = new CountryService.CountryDto[]
                    {
                        new CountryService.CountryDto
                        {
                            CountryRegionCode = "US",
                            Name = "United States"
                        },
                        new CountryService.CountryDto
                        {
                            CountryRegionCode = "GB",
                            Name = "England"
                        }
                    }
                });

            var sut = new CountryServiceWCF(
                mockLogger.Object,
                mapper,
                mockCountryService.Object
            );

            //Act
            var response = await sut.ListCountriesAsync();

            //Assert
            mockCountryService.Verify(x => x.ListCountriesAsync());
            response.Countries[0].CountryRegionCode.Should().Be("US");
            response.Countries[1].CountryRegionCode.Should().Be("GB");
        }
    }
}