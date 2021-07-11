using AutoFixture.Xunit2;
using AW.Services.ReferenceData.Core;
using AW.Services.ReferenceData.Core.Handlers.AddressType.GetAddressTypes;
using AW.SharedKernel.UnitTesting;
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
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ListAddressTypes_ReturnsAddressTypes(
            List<AddressType> addressTypes,
            [Frozen] Mock<IMediator> mockMediator,
            AddressTypeService sut
        )
        {
            //Arrange
            mockMediator.Setup(x => x.Send(It.IsAny<GetAddressTypesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(addressTypes);

            //Act
            var result = await sut.ListAddressTypes();

            //Assert
            result.Should().NotBeNull();
            result.AddressTypes.Count().Should().Be(addressTypes.Count);
        }
    }
}