using AutoMapper;
using AW.Core.Abstractions.Api.AddressTypeApi.ListAddressTypes;
using AW.Infrastructure.Api.WCF.AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AW.Infrastructure.Api.WCF.UnitTests
{
    public class AddressTypeServiceAdapterUnitTests
    {
        [Fact]
        public async void ListAddressTypes_ReturnsAddressTypes()
        {
            //Arrange
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<AddressTypeProfile>())
                .CreateMapper();
            var mockLogger = new Mock<ILogger<AddressTypeServiceAdapter>>();
            var mockAddressTypeService = new Mock<AddressTypeService.IAddressTypeService>();
            mockAddressTypeService.Setup(x => x.ListAddressTypesAsync())
                .ReturnsAsync(new AddressTypeService.ListAddressTypesResponse
                {
                    AddressTypes = new string[] { "Home", "Main Office" }
                });

            var sut = new AddressTypeServiceAdapter(
                mockLogger.Object,
                mapper,
                mockAddressTypeService.Object
            );

            //Act
            var response = await sut.ListAddressTypesAsync();

            //Assert
            mockAddressTypeService.Verify(x => x.ListAddressTypesAsync());
            response.AddressTypes[0].Should().Be("Home");
            response.AddressTypes[1].Should().Be("Main Office");
        }
    }
}