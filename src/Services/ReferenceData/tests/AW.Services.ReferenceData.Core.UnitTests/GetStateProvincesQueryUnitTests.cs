using AW.Services.ReferenceData.Core.Specifications;
using AW.Services.ReferenceData.Core.Handlers.StateProvince.GetStatesProvinces;
using AW.SharedKernel.Interfaces;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AW.SharedKernel.UnitTesting;
using AutoFixture.Xunit2;

namespace AW.Services.ReferenceData.Core.UnitTests
{
    public class GetStateProvincesQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_NoFilter_ReturnAllStateProvinces(
            List<Entities.StateProvince> statesProvinces,
            [Frozen] Mock<IRepository<Entities.StateProvince>> stateProvinceRepoMock,
            GetStatesProvincesQueryHandler sut,
            GetStatesProvincesQuery query
        )
        {
            //Arrange
            stateProvinceRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(statesProvinces);

            query.CountryRegionCode = "";

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            stateProvinceRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
            
            for (int i = 0; i < result.Count; i++)
            {
                result[i].Name.Should().Be(statesProvinces[i].Name);
            }
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_StateProvincesExists_ReturnStateProvincesForCountry(
            List<Entities.StateProvince> statesProvinces,
            [Frozen] Mock<IRepository<Entities.StateProvince>> stateProvinceRepoMock,
            GetStatesProvincesQueryHandler sut,
            GetStatesProvincesQuery query
        )
        {
            //Arrange
            stateProvinceRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetStatesProvincesForCountrySpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(statesProvinces);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            stateProvinceRepoMock.Verify(x => x.ListAsync(
                It.IsAny<GetStatesProvincesForCountrySpecification>(),
                It.IsAny<CancellationToken>()
            ));

            for (int i = 0; i < result.Count; i++)
            {
                result[i].Name.Should().Be(statesProvinces[i].Name);
            }
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void Handle_NoStateProvincesExists_ThrowException(
            [Frozen] Mock<IRepository<Entities.StateProvince>> stateProvinceRepoMock,
            GetStatesProvincesQueryHandler sut,
            GetStatesProvincesQuery query
        )
        {
            //Arrange
            stateProvinceRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync((List<Entities.StateProvince>)null);

            query.CountryRegionCode = "";

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>();
            stateProvinceRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
        }
    }
}