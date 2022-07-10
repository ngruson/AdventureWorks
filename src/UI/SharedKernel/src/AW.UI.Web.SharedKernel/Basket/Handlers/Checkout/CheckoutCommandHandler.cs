using Ardalis.GuardClauses;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Basket.Handlers.Checkout
{
    public class CheckoutCommandHandler : IRequestHandler<CheckoutCommand>
    {
        private readonly ILogger<CheckoutCommandHandler> logger;
        private readonly IBasketApiClient client;

        public CheckoutCommandHandler(ILogger<CheckoutCommandHandler> logger, IBasketApiClient client)
        {
            this.logger = logger;
            this.client = client;
        }

        public async Task<Unit> Handle(CheckoutCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request.Basket, nameof(request.Basket));

            logger.LogInformation("Checking out shopping basket for user ID {UserID}", request.Basket.Buyer);
            await client.CheckoutAsync(request.Basket);

            logger.LogInformation("Succesfully checked out shopping basket for user ID {UserID}", request.Basket.Buyer);

            return Unit.Value;
        }
    }
}