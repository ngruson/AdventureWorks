using AutoFixture.Xunit2;
using AW.SharedKernel.Caching;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetAddressTypes;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.ReferenceData.Handlers
{
    public class GetAddressTypesQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_ReturnAddressTypes(
            List<AddressType> addressTypes,
            [Frozen] Mock<ICache<AddressType>> cacheMock,
            GetAddressTypesQueryHandler sut,
            GetAddressTypesQuery query
        )
        {
            //Arrange
            cacheMock.Setup(x => x.GetData())
                .ReturnsAsync(addressTypes);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            cacheMock.Verify(x => x.GetData());
            result.Should().BeEquivalentTo(addressTypes);
        }
    }
}
