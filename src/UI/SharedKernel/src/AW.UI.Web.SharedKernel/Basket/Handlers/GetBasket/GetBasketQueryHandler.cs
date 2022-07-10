using Ardalis.GuardClauses;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Basket.Handlers.GetBasket
{
    public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, Basket>
    {
        private readonly ILogger<GetBasketQueryHandler> logger;
        private readonly IBasketApiClient client;

        public GetBasketQueryHandler(ILogger<GetBasketQueryHandler> logger, IBasketApiClient client)
        {
            this.logger = logger;
            this.client = client;
        }

        public async Task<Basket> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(request.UserID, nameof(request.UserID));

            logger.LogInformation("Getting shopping basket for user ID {UserID} from API", request.UserID);
            var basket = await client.GetBasketAsync(request.UserID);
            Guard.Against.Null(basket, nameof(basket));

            logger.LogInformation("Returning shopping basket for user ID {UserID}", request.UserID);

            return basket;
        }
    }
}