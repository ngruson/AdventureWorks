using AW.Application.Country.ListCountries;
using AW.Application.Exceptions;
using Ardalis.Specification;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Application.UnitTests
{
    public class ListCountriesQueryHandlerUnitTests
    {
        [Fact]
        public async void ListCountries_CountriesExist_ReturnCountries()
        {
            // Arrange
            var countries = new List<Domain.Person.CountryRegion> {
                new Domain.Person.CountryRegion {  Name = "United Kingdom"},
                new Domain.Person.CountryRegion {  Name = "United States"},
                new Domain.Person.CountryRegion {  Name = "Germany"}
            };

            var repoMock = new Mock<IRepositoryBase<Domain.Person.CountryRegion>>();
            repoMock.Setup(x => x.ListAsync())
                .ReturnsAsync(countries);

            var handler = new ListCountriesQueryHandler(repoMock.Object);

            //Act
            var result = await handler.Handle(new ListCountriesQuery(), CancellationToken.None);

            //Assert
            result.Count().Should().Be(3);
        }

        [Fact]
        public void ListCountries_NoCountriesExist_ThrowException()
        {
            // Arrange
            var countries = new List<Domain.Person.CountryRegion>();

            var repoMock = new Mock<IRepositoryBase<Domain.Person.CountryRegion>>();
            repoMock.Setup(x => x.ListAsync())
                .ReturnsAsync(countries);

            var handler = new ListCountriesQueryHandler(repoMock.Object);

            //Act
            Func<Task> func = async () => await handler.Handle(new ListCountriesQuery(), CancellationToken.None);

            //Assert
            func.Should().Throw<CountriesNotFoundException>();
        }
    }
}