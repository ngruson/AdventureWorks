using Ardalis.Specification;
using AW.Services.ReferenceData.Application.Specifications;
using AW.Services.ReferenceData.Application.StateProvince.GetStatesProvinces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.ReferenceData.Application.UnitTests
{
    public class GetStateProvincesQueryUnitTests
    {
        [Fact]
        public async void Handle_StateProvincesExists_ReturnStateProvinces()
        {
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<GetStatesProvincesQueryHandler>>();
            var stateProvinceRepoMock = new Mock<IRepositoryBase<Domain.StateProvince>>();

            stateProvinceRepoMock.Setup(x => x.ListAsync())
                .ReturnsAsync(new List<Domain.StateProvince>
                {
                    new TestBuilders.StateProvinceBuilder()
                        .WithTestValues()
                        .Build(),

                    new TestBuilders.StateProvinceBuilder()
                        .StateProvinceCode("BC")
                        .Name("British Columbia")
                        .CountryRegionCode("CA")
                        .CountryRegion(new Domain.CountryRegion 
                            { 
                                CountryRegionCode = "CA",
                                Name = "Canada"
                            }
                        )
                        .Build()
                });

            var handler = new GetStatesProvincesQueryHandler(
                loggerMock.Object,
                stateProvinceRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetStatesProvincesQuery();
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            stateProvinceRepoMock.Verify(x => x.ListAsync());
            result[0].Name.Should().Be("Alberta");
            result[1].Name.Should().Be("British Columbia");
        }

        [Fact]
        public async void Handle_StateProvincesExists_ReturnStateProvincesForCountry()
        {
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<GetStatesProvincesQueryHandler>>();
            var stateProvinceRepoMock = new Mock<IRepositoryBase<Domain.StateProvince>>();

            stateProvinceRepoMock.Setup(x => x.ListAsync(It.IsAny<GetStatesProvincesForCountrySpecification>()))
                .ReturnsAsync(new List<Domain.StateProvince>
                {
                    new TestBuilders.StateProvinceBuilder()
                        .WithTestValues()
                        .Build(),

                    new TestBuilders.StateProvinceBuilder()
                        .StateProvinceCode("BC")
                        .Name("British Columbia")
                        .CountryRegionCode("CA")
                        .CountryRegion(new Domain.CountryRegion
                            {
                                CountryRegionCode = "CA",
                                Name = "Canada"
                            }
                        )
                        .Build()
                });

            var handler = new GetStatesProvincesQueryHandler(
                loggerMock.Object,
                stateProvinceRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetStatesProvincesQuery { CountryRegionCode = "CA" };
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            stateProvinceRepoMock.Verify(x => x.ListAsync(It.IsAny<GetStatesProvincesForCountrySpecification>()));
            result[0].Name.Should().Be("Alberta");
            result[1].Name.Should().Be("British Columbia");
        }

        [Fact]
        public void Handle_NoStateProvincesExists_ThrowException()
        {
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<GetStatesProvincesQueryHandler>>();
            var contactTypeRepoMock = new Mock<IRepositoryBase<Domain.StateProvince>>();

            var handler = new GetStatesProvincesQueryHandler(
                loggerMock.Object,
                contactTypeRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetStatesProvincesQuery();
            Func<Task> func = async () => await handler.Handle(query, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>();
            contactTypeRepoMock.Verify(x => x.ListAsync());
        }
    }
}