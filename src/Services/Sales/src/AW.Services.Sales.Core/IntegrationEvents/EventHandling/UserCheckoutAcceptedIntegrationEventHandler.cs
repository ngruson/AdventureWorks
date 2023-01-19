using AutoMapper;
using AW.Services.Sales.Core.Handlers.CreateSalesOrder;
using AW.Services.Sales.Core.IntegrationEvents.Events;
using AW.Services.Sales.Core.Handlers.Identified;
using AW.Services.Infrastructure.EventBus.Abstractions;
using AW.Services.Infrastructure.EventBus.Extensions;
using AW.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Threading.Tasks;

namespace AW.Services.Sales.Core.IntegrationEvents.EventHandling
{
    public class UserCheckoutAcceptedIntegrationEventHandler : IIntegrationEventHandler<UserCheckoutAcceptedIntegrationEvent>
    {
        private readonly IApplication application;
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly ILogger<UserCheckoutAcceptedIntegrationEventHandler> logger;

        public UserCheckoutAcceptedIntegrationEventHandler(
            IApplication application,
            IMediator mediator,
            IMapper mapper,
            ILogger<UserCheckoutAcceptedIntegrationEventHandler> logger)
        {
            this.application = application ?? throw new ArgumentNullException(nameof(application));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Integration event handler which starts the create order process
        /// </summary>
        /// <param name="@event">
        /// Integration event message which is sent by the
        /// basket.api once it has successfully process the 
        /// order items.
        /// </param>
        /// <returns></returns>
        public async Task Handle(UserCheckoutAcceptedIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{application.AppName}"))
            {
                logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, application.AppName, @event);

                var result = false;

                if (@event.RequestId != Guid.Empty)
                {
                    using (LogContext.PushProperty("IdentifiedCommandId", @event.RequestId))
                    {
                        var createSalesOrderCommand = new CreateSalesOrderCommand(
                            @event.BasketItems, @event.UserId, @event.UserName,
                            null,
                            @event.CustomerNumber, @event.ShipMethod,
                            mapper.Map<AddressDto>(@event.BillToAddress),
                            mapper.Map<AddressDto>(@event.ShipToAddress),
                            @event.CardNumber, @event.CardHolderName, @event.CardExpiration,
                            @event.CardSecurityNumber, @event.CardType
                        );

                        var requestCreateOrder = new IdentifiedCommand<CreateSalesOrderCommand, bool>(createSalesOrderCommand, @event.RequestId);

                        logger.LogInformation(
                            "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                            requestCreateOrder.GetGenericTypeName(),
                            nameof(requestCreateOrder.Id),
                            requestCreateOrder.Id,
                            requestCreateOrder
                        );

                        result = await mediator.Send(requestCreateOrder);

                        if (result)
                        {
                            logger.LogInformation("----- CreateSalesOrderCommand succeeded - RequestId: {RequestId}", @event.RequestId);
                        }
                        else
                        {
                            logger.LogWarning("CreateSalesOrderCommand failed - RequestId: {RequestId}", @event.RequestId);
                        }
                    }
                }
                else
                {
                    logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", @event);
                }
            }
        }
    }
}