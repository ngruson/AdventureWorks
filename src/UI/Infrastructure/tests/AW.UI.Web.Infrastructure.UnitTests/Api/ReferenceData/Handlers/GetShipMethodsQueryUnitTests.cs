using AutoFixture.Xunit2;
using AW.SharedKernel.Caching;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetShipMethods;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.ReferenceData.Handlers
{
    public class GetShipMethodsQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_ReturnShipMethods(
            List<ShipMethod> shipMethods,
            [Frozen] Mock<ICache<ShipMethod>> cacheMock,
            GetShipMethodsQueryHandler sut,
            GetShipMethodsQuery query
        )
        {
            //Arrange
            cacheMock.Setup(x => x.GetData())
                .ReturnsAsync(shipMethods);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            cacheMock.Verify(x => x.GetData());
            result.Should().BeEquivalentTo(shipMethods);
        }
    }
}
