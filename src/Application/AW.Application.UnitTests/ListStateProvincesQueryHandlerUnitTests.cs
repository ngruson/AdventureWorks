using AW.Application.Interfaces;
using AW.Application.Specifications;
using AW.Application.StateProvince.ListStateProvinces;
using AW.Application.UnitTests.AutoMapper;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace AW.Application.UnitTests
{
    public class ListStateProvincesQueryHandlerUnitTests
    {
        [Fact]
        public async void ListStateProvinces_StateProvincesExist_ReturnStateProvinces()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var stateProvinces = new List<Domain.Person.StateProvince> {
                new Domain.Person.StateProvince { StateProvinceCode = "AB" },
                new Domain.Person.StateProvince { StateProvinceCode = "AK" },
                new Domain.Person.StateProvince { StateProvinceCode = "AL" }
            };

            var repoMock = new Mock<IAsyncRepository<Domain.Person.StateProvince>>();
            repoMock.Setup(x => x.ListAllAsync())
                .ReturnsAsync(stateProvinces);

            var handler = new ListStateProvincesQueryHandler(mapper, repoMock.Object);

            //Act
            var result = await handler.Handle(new ListStateProvincesQuery(), CancellationToken.None);

            //Assert
            result.Count().Should().Be(3);
        }

        [Fact]
        public async void ListStateProvinces_FilterByCountry_ReturnStateProvinces()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var stateProvinces = new List<Domain.Person.StateProvince> {                
                new Domain.Person.StateProvince { StateProvinceCode = "AK" },
                new Domain.Person.StateProvince { StateProvinceCode = "AL" },
                new Domain.Person.StateProvince { StateProvinceCode = "AR" }
            };

            var repoMock = new Mock<IAsyncRepository<Domain.Person.StateProvince>>();
            repoMock.Setup(x => x.ListAsync(It.IsAny<ListStateProvincesSpecification>()))
                .ReturnsAsync(stateProvinces);

            var handler = new ListStateProvincesQueryHandler(mapper, repoMock.Object);
            var query = new ListStateProvincesQuery
            {
                CountryRegionCode = "US"
            };
            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Count().Should().Be(3);
        }
    }
}