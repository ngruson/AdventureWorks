using Ardalis.GuardClauses;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Product.Handlers.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product>
    {
        private readonly ILogger<GetProductQueryHandler> logger;
        private readonly IProductApiClient client;

        public GetProductQueryHandler(ILogger<GetProductQueryHandler> logger, IProductApiClient client)
        {
            this.logger = logger;
            this.client = client;
        }

        public async Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting product {ProductNumber} from API", request.ProductNumber);
            var product = await client.GetProductAsync(
                request.ProductNumber
            );
            Guard.Against.Null(product, nameof(product));

            logger.LogInformation("Returning product {ProductNumber}", request.ProductNumber);

            return product;
        }
    }
}