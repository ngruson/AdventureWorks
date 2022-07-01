using AutoFixture.Xunit2;
using AW.SharedKernel.Caching;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetShipMethods;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.ReferenceData.Handlers
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