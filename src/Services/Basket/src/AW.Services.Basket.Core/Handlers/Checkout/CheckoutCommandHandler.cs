using AW.Services.Basket.Core.IntegrationEvents.Events;
using AW.Services.Basket.Core.Model;
using AW.SharedKernel.Api.EventBus.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Basket.Core.Handlers.Checkout
{
    public class CheckoutCommandHandler : IRequestHandler<CheckoutCommand, CustomerBasket>
    {
        private readonly ILogger<CheckoutCommandHandler> logger;
        private readonly IIdentityService identityService;
        private readonly IBasketRepository repository;
        private readonly IEventBus eventBus;

        public CheckoutCommandHandler(ILogger<CheckoutCommandHandler> logger, IIdentityService identityService, IBasketRepository repository, IEventBus eventBus) =>
            (this.logger, this.identityService, this.repository, this.eventBus) = (logger, identityService, repository, eventBus);

        public async Task<CustomerBasket> Handle(CheckoutCommand request, CancellationToken cancellationToken)
        {
            var userId = identityService.GetUserIdentity();
            logger.LogInformation("Retrieved identity for {UserId}", userId);

            logger.LogInformation("Getting basket for {UserId}", userId);
            var basket = await repository.GetBasketAsync(userId);

            if (basket == null)
                return basket;

            var userName = identityService.GetUserName();

            var eventMessage = new UserCheckoutAcceptedIntegrationEvent(
                userId, userName, request.BasketCheckout.City, request.BasketCheckout.Street,
                request.BasketCheckout.State, request.BasketCheckout.Country, request.BasketCheckout.ZipCode, request.BasketCheckout.CardNumber, request.BasketCheckout.CardHolderName,
                request.BasketCheckout.CardExpiration, request.BasketCheckout.CardSecurityNumber, request.BasketCheckout.CardTypeId, request.BasketCheckout.Buyer, request.BasketCheckout.RequestId, basket);

            // Once basket is checkout, sends an integration event to
            // ordering.api to convert basket to order and proceeds with
            // order creation process
            try
            {
                logger.LogInformation("Publishing UserCheckoutAccepted integration event");
                eventBus.Publish(eventMessage);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId}", eventMessage.Id);

                throw;
            }

            return basket;
        }
    }
}