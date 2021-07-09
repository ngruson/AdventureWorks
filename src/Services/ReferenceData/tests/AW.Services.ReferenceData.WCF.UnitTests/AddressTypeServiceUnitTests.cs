using AW.Services.ReferenceData.Core.Handlers.AddressType.GetAddressTypes;
using FluentAssertions;
using MediatR;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.ReferenceData.WCF.UnitTests
{
    public class AddressTypeServiceUnitTests
    {
        [Fact]
        public async Task ListAddressTypes_ReturnsAddressTypes()
        {
            //Arrange
            var addressTypes = new List<AddressType>
            {
                new AddressType { Name = "Home" },
                new AddressType { Name = "Main Office"}
            };

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetAddressTypesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(addressTypes);

            var addressTypeService = new AddressTypeService(
                mockMediator.Object
            );

            //Act
            var result = await addressTypeService.ListAddressTypes();

            //Assert
            result.Should().NotBeNull();
            result.AddressTypes.Count().Should().Be(2);
        }
    }
}