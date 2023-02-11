using AW.Services.Basket.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Basket.Core.Handlers.GetBasket
{
    public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, CustomerBasket?>
    {
        private readonly ILogger<GetBasketQueryHandler> logger;
        private readonly IBasketRepository repository;

        public GetBasketQueryHandler(ILogger<GetBasketQueryHandler> logger, IBasketRepository repository) =>
            (this.logger, this.repository) = (logger, repository);
        
        public async Task<CustomerBasket?> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting basket for {UserId}", request.Id);
            var basket = await repository.GetBasketAsync(request.Id);

            logger.LogInformation("Returning basket");
            return basket;
        }
    }
}