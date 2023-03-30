using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Product.Handlers.DuplicateProduct
{
    public class DuplicateProductCommandHandler : IRequestHandler<DuplicateProductCommand, Product>
    {
        private readonly ILogger<DuplicateProductCommandHandler> _logger;
        private readonly IProductApiClient _client;

        public DuplicateProductCommandHandler(ILogger<DuplicateProductCommandHandler> logger, IProductApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<Product> Handle(DuplicateProductCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(request.ProductNumber, _logger);

            _logger.LogInformation("Updating product");
            var product = await _client.DuplicateProduct(request.ProductNumber);
            _logger.LogInformation("Product succesfully updated");

            return product;
        }
    }
}
