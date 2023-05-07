using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.Basket.Handlers.UpdateBasket
{
    public class UpdateBasketCommandHandler : IRequestHandler<UpdateBasketCommand, Basket?>
    {
        private readonly ILogger<UpdateBasketCommandHandler> _logger;
        private readonly IBasketApiClient _client;

        public UpdateBasketCommandHandler(ILogger<UpdateBasketCommandHandler> logger, IBasketApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<Basket?> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request.Basket, _logger);

            _logger.LogInformation("Updating shopping basket for user ID {UserID}", request.Basket?.BuyerId);
            var basket = await _client.UpdateBasketAsync(request.Basket);
            Guard.Against.Null(basket, _logger);

            _logger.LogInformation("Returning shopping basket for user ID {UserID}", request.Basket?.BuyerId);

            return basket;
        }
    }
}
