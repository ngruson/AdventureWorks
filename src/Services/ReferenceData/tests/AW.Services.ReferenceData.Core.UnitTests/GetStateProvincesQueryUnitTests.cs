using Ardalis.Specification;
using AW.Services.ReferenceData.Core.Specifications;
using AW.Services.ReferenceData.Core.Handlers.StateProvince.GetStatesProvinces;
using AW.SharedKernel.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.ReferenceData.Core.UnitTests
{
    public class GetStateProvincesQueryUnitTests
    {
        [Fact]
        public async void Handle_StateProvincesExists_ReturnStateProvinces()
        {
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<GetStatesProvincesQueryHandler>>();
            var stateProvinceRepoMock = new Mock<IRepository<Entities.StateProvince>>();

            stateProvinceRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Entities.StateProvince>
                {
                    new TestBuilders.StateProvinceBuilder()
                        .WithTestValues()
                        .Build(),

                    new TestBuilders.StateProvinceBuilder()
                        .StateProvinceCode("BC")
                        .Name("British Columbia")
                        .CountryRegionCode("CA")
                        .CountryRegion(new Entities.CountryRegion 
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
            stateProvinceRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
            result[0].Name.Should().Be("Alberta");
            result[1].Name.Should().Be("British Columbia");
        }

        [Fact]
        public async void Handle_StateProvincesExists_ReturnStateProvincesForCountry()
        {
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<GetStatesProvincesQueryHandler>>();
            var stateProvinceRepoMock = new Mock<IRepository<Entities.StateProvince>>();

            stateProvinceRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetStatesProvincesForCountrySpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new List<Entities.StateProvince>
            {
                new TestBuilders.StateProvinceBuilder()
                    .WithTestValues()
                    .Build(),

                new TestBuilders.StateProvinceBuilder()
                    .StateProvinceCode("BC")
                    .Name("British Columbia")
                    .CountryRegionCode("CA")
                    .CountryRegion(new Entities.CountryRegion
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
            stateProvinceRepoMock.Verify(x => x.ListAsync(
                It.IsAny<GetStatesProvincesForCountrySpecification>(),
                It.IsAny<CancellationToken>()
            ));
            result[0].Name.Should().Be("Alberta");
            result[1].Name.Should().Be("British Columbia");
        }

        [Fact]
        public void Handle_NoStateProvincesExists_ThrowException()
        {
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<GetStatesProvincesQueryHandler>>();
            var stateProvinceRepoMock = new Mock<IRepository<Entities.StateProvince>>();

            var handler = new GetStatesProvincesQueryHandler(
                loggerMock.Object,
                stateProvinceRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetStatesProvincesQuery();
            Func<Task> func = async () => await handler.Handle(query, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>();
            stateProvinceRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
        }
    }
}