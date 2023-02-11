using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Basket.Handlers.GetBasket
{
    public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, Basket>
    {
        private readonly ILogger<GetBasketQueryHandler> _logger;
        private readonly IBasketApiClient _client;

        public GetBasketQueryHandler(ILogger<GetBasketQueryHandler> logger, IBasketApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<Basket> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(request.UserID, _logger);

            _logger.LogInformation("Getting shopping basket for user ID {UserID} from API", request.UserID);
            var basket = await _client.GetBasketAsync(request.UserID);
            Guard.Against.Null(basket, _logger);

            _logger.LogInformation("Returning shopping basket for user ID {UserID}", request.UserID);

            return basket!;
        }
    }
}