using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Product.Handlers.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product?>
    {
        private readonly ILogger<GetProductQueryHandler> _logger;
        private readonly IProductApiClient _client;

        public GetProductQueryHandler(ILogger<GetProductQueryHandler> logger, IProductApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<Product?> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting product {ProductNumber} from API", request.ProductNumber);
            var product = await _client.GetProduct(
                request.ProductNumber
            );
            Guard.Against.Null(product, _logger);

            _logger.LogInformation("Returning product {ProductNumber}", request.ProductNumber);

            return product;
        }
    }
}
