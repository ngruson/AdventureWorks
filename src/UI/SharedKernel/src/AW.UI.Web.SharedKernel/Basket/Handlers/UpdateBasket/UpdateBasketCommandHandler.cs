using Ardalis.GuardClauses;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Basket.Handlers.UpdateBasket
{
    public class UpdateBasketCommandHandler : IRequestHandler<UpdateBasketCommand, Basket>
    {
        private readonly ILogger<UpdateBasketCommandHandler> logger;
        private readonly IBasketApiClient client;

        public UpdateBasketCommandHandler(ILogger<UpdateBasketCommandHandler> logger, IBasketApiClient client)
        {
            this.logger = logger;
            this.client = client;
        }

        public async Task<Basket> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request.Basket, nameof(request.Basket));

            logger.LogInformation("Updating shopping basket for user ID {UserID}", request.Basket?.BuyerId);
            var basket = await client.UpdateBasket(request.Basket);
            Guard.Against.Null(basket, nameof(basket));

            logger.LogInformation("Returning shopping basket for user ID {UserID}", request.Basket?.BuyerId);

            return basket;
        }
    }
}