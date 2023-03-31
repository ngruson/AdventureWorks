using Ardalis.GuardClauses;
using AW.Services.Product.Core.GuardClauses;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Product.Core.Handlers.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly ILogger<DeleteProductCommandHandler> _logger;
        private readonly IRepository<Entities.Product> _repository;

        public DeleteProductCommandHandler(
            ILogger<DeleteProductCommandHandler> logger,
            IRepository<Entities.Product> salesOrderRepository
        ) => (_logger, _repository) = (logger, salesOrderRepository);

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting product");
            var product = await _repository.SingleOrDefaultAsync(
                new GetProductSpecification(request.ProductNumber),
                cancellationToken
            );
            Guard.Against.ProductNull(product, request.ProductNumber!, _logger);

            _logger.LogInformation("Deleting product");
            await _repository.DeleteAsync(product!, cancellationToken);

            _logger.LogInformation("Product succesfully deleted");
        }
    }
}
