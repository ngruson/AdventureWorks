using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetAddressTypes;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetContactTypes;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetCountries;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetStateProvinces;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetTerritories;
using AW.UI.Web.Internal.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AW.UI.Web.Internal.UnitTests.Services
{
    public class ReferenceDataServiceUnitTests
    {
        [Theory, AutoMoqData]
        public async void GetAddressTypes_AddressTypesFound_ReturnsAddressTypes(
                [Frozen] Mock<IReferenceDataApiClient> referenceDataApiClient,
                List<AddressType> addressTypes,
                ReferenceDataService sut
            )
        {
            //Arrange
            referenceDataApiClient.Setup(x => x.GetAddressTypesAsync())
                .ReturnsAsync(addressTypes);
            
            //Act
            var response = await sut.GetAddressTypesAsync();

            //Assert
            response.Count.Should().Be(3);
        }

        [Theory, AutoMoqData]
        public void GetAddressTypes_NoAddressTypesFound_ReturnsAddressTypes(
                [Frozen] Mock<IReferenceDataApiClient> referenceDataApiClient,
                ReferenceDataService sut
            )
        {
            //Arrange
            referenceDataApiClient.Setup(x => x.GetAddressTypesAsync())
                .ReturnsAsync((List<AddressType>)null);

            //Act
            Func<Task> func = async () => await sut.GetAddressTypesAsync();

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'addressTypes')");
        }

        [Theory, AutoMoqData]
        public async void GetContactTypes_ContactTypesFound_ReturnsContactTypes(
                [Frozen] Mock<IReferenceDataApiClient> referenceDataApiClient,
                List<ContactType> contactTypes,
                ReferenceDataService sut
            )
        {
            //Arrange
            referenceDataApiClient.Setup(x => x.GetContactTypesAsync())
                .ReturnsAsync(contactTypes);

            //Act
            var response = await sut.GetContactTypesAsync();

            //Assert
            response.Count.Should().Be(3);
        }

        [Theory, AutoMoqData]
        public void GetContactTypes_NoContactTypesFound_ReturnsContactTypes(
                [Frozen] Mock<IReferenceDataApiClient> referenceDataApiClient,
                ReferenceDataService sut
            )
        {
            //Arrange
            referenceDataApiClient.Setup(x => x.GetContactTypesAsync())
                .ReturnsAsync((List<ContactType>)null);

            //Act
            Func<Task> func = async () => await sut.GetContactTypesAsync();

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'contactTypes')");
        }

        [Theory, AutoMoqData]
        public async void GetCountries_CountriesFound_ReturnsCountries(
                [Frozen] Mock<IReferenceDataApiClient> referenceDataApiClient,
                List<CountryRegion> countries,
                ReferenceDataService sut
            )
        {
            //Arrange
            referenceDataApiClient.Setup(x => x.GetCountriesAsync())
                .ReturnsAsync(countries);

            //Act
            var response = await sut.GetCountriesAsync();

            //Assert
            response.Count.Should().Be(3);
        }

        [Theory, AutoMoqData]
        public void GetCountries_NoCountriesFound_ReturnsCountries(
                [Frozen] Mock<IReferenceDataApiClient> referenceDataApiClient,
                ReferenceDataService sut
            )
        {
            //Arrange
            referenceDataApiClient.Setup(x => x.GetCountriesAsync())
                .ReturnsAsync((List<CountryRegion>)null);

            //Act
            Func<Task> func = async () => await sut.GetCountriesAsync();

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'countries')");
        }

        [Theory, AutoMoqData]
        public async void GetStatesProvinces_StatesProvincesFound_ReturnsStatesProvinces(
                [Frozen] Mock<IReferenceDataApiClient> referenceDataApiClient,
                List<StateProvince> statesProvinces,
                ReferenceDataService sut
            )
        {
            //Arrange
            referenceDataApiClient.Setup(x => x.GetStatesProvincesAsync(
                It.IsAny<string>()
            ))
            .ReturnsAsync(statesProvinces);

            //Act
            var response = await sut.GetStatesProvincesAsync();

            //Assert
            response.Count.Should().Be(3);
        }

        [Theory, AutoMoqData]
        public void GetStatesProvinces_NoStatesProvincesFound_ReturnsStatesProvinces(
                [Frozen] Mock<IReferenceDataApiClient> referenceDataApiClient,
                ReferenceDataService sut
            )
        {
            //Arrange
            referenceDataApiClient.Setup(x => x.GetStatesProvincesAsync(
                It.IsAny<string>()
            ))
            .ReturnsAsync((List<StateProvince>)null);

            //Act
            Func<Task> func = async () => await sut.GetStatesProvincesAsync();

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'statesProvinces')");
        }

        [Theory, AutoMoqData]
        public async void GetTerritories_TerritoriesFound_ReturnsTerritories(
                [Frozen] Mock<IReferenceDataApiClient> referenceDataApiClient,
                List<Territory> territories,
                ReferenceDataService sut
            )
        {
            //Arrange
            referenceDataApiClient.Setup(x => x.GetTerritoriesAsync())
                .ReturnsAsync(territories);

            //Act
            var response = await sut.GetTerritoriesAsync();

            //Assert
            response.Count.Should().Be(3);
        }

        [Theory, AutoMoqData]
        public void GetTerritories_NoTerritoriesFound_ReturnsTerritories(
                [Frozen] Mock<IReferenceDataApiClient> referenceDataApiClient,
                ReferenceDataService sut
            )
        {
            //Arrange
            referenceDataApiClient.Setup(x => x.GetTerritoriesAsync())
                .ReturnsAsync((List<Territory>)null);

            //Act
            Func<Task> func = async () => await sut.GetTerritoriesAsync();

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'territories')");
        }
    }
}