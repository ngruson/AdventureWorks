using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Basket.Core.Handlers.DeleteBasket
{
    public class DeleteBasketCommandHandler : IRequestHandler<DeleteBasketCommand, Unit>
    {
        private readonly ILogger<DeleteBasketCommandHandler> logger;
        private readonly IBasketRepository repository;

        public DeleteBasketCommandHandler(ILogger<DeleteBasketCommandHandler> logger, IBasketRepository repository) =>
            (this.logger, this.repository) = (logger, repository);

        public async Task<Unit> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting basket for {UserId}", request.Id);
            await repository.DeleteBasketAsync(request.Id);

            return Unit.Value;
        }
    }
}
