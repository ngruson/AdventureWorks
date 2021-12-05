using AutoFixture.Xunit2;
using AutoMapper;
using AW.Services.SalesOrder.Core.IntegrationEvents.EventHandling;
using AW.Services.SalesOrder.Core.IntegrationEvents.Events;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.SalesOrder.Core.UnitTests.IntegrationEvents
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

        [Fact]
        public void Create_ApplicationParamIsNull_ThrowArgumentNullException()
        {
            //Arrange

            //Act
            Action act = () => _ = new UserCheckoutAcceptedIntegrationEventHandler(
                null, 
                null, 
                null, 
                null
            );

            //Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Theory, AutoMoqData]
        public void Create_MediatorParamIsNull_ThrowArgumentNullException(
            Mock<IApplication> mockApplication
        )
        {
            //Arrange

            //Act
            Action act = () => _ = new UserCheckoutAcceptedIntegrationEventHandler(
                mockApplication.Object,
                null, null, null
            );

            //Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Theory, AutoMoqData]
        public void Create_MapperParamIsNull_ThrowArgumentNullException(
            Mock<IApplication> mockApplication,
            Mock<IMediator> mockMediator
        )
        {
            //Arrange

            //Act
            Action act = () => _ = new UserCheckoutAcceptedIntegrationEventHandler(
                mockApplication.Object,
                mockMediator.Object, 
                null, null
            );

            //Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Theory, AutoMoqData]
        public void Create_LoggerParamIsNull_ThrowArgumentNullException(
            Mock<IApplication> mockApplication,
            Mock<IMediator> mockMediator,
            Mock<IMapper> mockMapper
        )
        {
            //Arrange

            //Act
            Action act = () => _ = new UserCheckoutAcceptedIntegrationEventHandler(
                mockApplication.Object,
                mockMediator.Object,
                mockMapper.Object, 
                null
            );

            //Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}