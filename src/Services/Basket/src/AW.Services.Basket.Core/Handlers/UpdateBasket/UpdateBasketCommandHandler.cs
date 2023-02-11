using AW.Services.Basket.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Basket.Core.Handlers.UpdateBasket
{
    public class UpdateBasketCommandHandler : IRequestHandler<UpdateBasketCommand, CustomerBasket>
    {
        private readonly ILogger<UpdateBasketCommandHandler> logger;
        private readonly IBasketRepository repository;

        public UpdateBasketCommandHandler(ILogger<UpdateBasketCommandHandler> logger, IBasketRepository repository) =>
            (this.logger, this.repository) = (logger, repository);

        public async Task<CustomerBasket> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating basket for {UserId}", request.Basket.BuyerId);
            var basket = await repository.UpdateBasketAsync(request.Basket);

            logger.LogInformation("Returning basket");
            return basket!;
        }
    }
}
