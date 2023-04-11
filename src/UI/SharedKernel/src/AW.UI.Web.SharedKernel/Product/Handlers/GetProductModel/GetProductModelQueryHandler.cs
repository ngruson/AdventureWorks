using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Product.Handlers.GetProductModel
{
    public class GetProductModelQueryHandler : IRequestHandler<GetProductModelQuery, ProductModel?>
    {
        private readonly ILogger<GetProductModelQueryHandler> _logger;
        private readonly IProductApiClient _client;

        public GetProductModelQueryHandler(ILogger<GetProductModelQueryHandler> logger, IProductApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<ProductModel?> Handle(GetProductModelQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting product model from API");

            var productModel = await _client.GetProductModel(request.Name);
            Guard.Against.Null(productModel, _logger);

            _logger.LogInformation("Returning product model");

            return productModel;
        }
    }
}
