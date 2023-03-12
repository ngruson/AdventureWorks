using AutoFixture.Xunit2;
using AW.Services.Sales.Core.AutoMapper;
using AW.Services.Sales.Core.IntegrationEvents.EventHandling;
using AW.Services.Sales.Core.IntegrationEvents.Events.UserCheckoutAccepted;
using AW.SharedKernel.UnitTesting;
using MediatR;
using Moq;
using Xunit;

namespace AW.Services.Sales.Core.UnitTests.IntegrationEvents
{
    public class UserCheckoutAcceptedIntegrationEventHandlerUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_GuidNotEmpty_CommandIsSent(
            [Frozen] Mock<IMediator> mockMediator,
            UserCheckoutAcceptedIntegrationEventHandler sut,
            UserCheckoutAcceptedIntegrationEvent integrationEvent
        )
        {
            //Arrange

            //Act
            await sut.Handle(integrationEvent);

            //Assert
            mockMediator.Verify(_ => _.Send(
                It.IsAny<IRequest<bool>>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_GuidEmpty_CommandIsNotSent(
            [Frozen] Mock<IMediator> mockMediator,
            UserCheckoutAcceptedIntegrationEventHandler sut,
            UserCheckoutAcceptedIntegrationEvent integrationEvent
        )
        {
            //Arrange
            integrationEvent.RequestId = Guid.Empty;

            //Act
            await sut.Handle(integrationEvent);

            //Assert
            mockMediator.Verify(_ => _.Send(
                It.IsAny<IRequest<bool>>(),
                It.IsAny<CancellationToken>()
            ), Times.Never);
        }
    }
}