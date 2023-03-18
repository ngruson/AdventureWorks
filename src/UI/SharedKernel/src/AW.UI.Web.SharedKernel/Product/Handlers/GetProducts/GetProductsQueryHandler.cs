using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Product.Handlers.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GetProductsResult>
    {
        private readonly ILogger<GetProductsQueryHandler> _logger;
        private readonly IProductApiClient _client;

        public GetProductsQueryHandler(ILogger<GetProductsQueryHandler> logger, IProductApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting products from API");
            var productsResult = await _client.GetProducts(
                request.PageIndex,
                request.PageSize,
                request.Category,
                request.Subcategory,
                request.OrderBy ?? "asc(productNumber)"
            );
            Guard.Against.Null(productsResult, _logger);

            _logger.LogInformation("Returning products");

            return productsResult!;
        }
    }
}
