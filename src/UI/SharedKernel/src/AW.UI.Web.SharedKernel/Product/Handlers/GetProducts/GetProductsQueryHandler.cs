using Ardalis.GuardClauses;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Product.Handlers.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GetProductsResult>
    {
        private readonly ILogger<GetProductsQueryHandler> logger;
        private readonly IProductApiClient client;

        public GetProductsQueryHandler(ILogger<GetProductsQueryHandler> logger, IProductApiClient client)
        {
            this.logger = logger;
            this.client = client;
        }

        public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting products from API");
            var productsResult = await client.GetProductsAsync(
                request.PageIndex,
                request.PageSize,
                request.Category,
                request.Subcategory,
                request.OrderBy ?? "asc(productNumber)"
            );
            Guard.Against.Null(productsResult, nameof(productsResult));

            logger.LogInformation("Returning products");

            return productsResult;
        }
    }
}