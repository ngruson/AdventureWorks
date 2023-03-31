using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Product.Handlers.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly ILogger<DeleteProductCommandHandler> _logger;
        private readonly IProductApiClient _client;

        public DeleteProductCommandHandler(ILogger<DeleteProductCommandHandler> logger, IProductApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(request.ProductNumber, _logger);

            _logger.LogInformation("Deleting product");
            await _client.DeleteProduct(request.ProductNumber);
            _logger.LogInformation("Product succesfully deleted");
        }
    }
}
