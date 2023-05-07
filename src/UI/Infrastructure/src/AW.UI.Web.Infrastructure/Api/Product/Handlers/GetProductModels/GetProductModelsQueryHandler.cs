using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProductModels
{
    public class GetProductModelsQueryHandler : IRequestHandler<GetProductModelsQuery, List<ProductModel>>
    {
        private readonly ILogger<GetProductModelsQueryHandler> _logger;
        private readonly IProductApiClient _client;

        public GetProductModelsQueryHandler(ILogger<GetProductModelsQueryHandler> logger, IProductApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<List<ProductModel>> Handle(GetProductModelsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting product models from API");

            var productModels = await _client.GetProductModels();
            Guard.Against.Null(productModels, _logger);

            _logger.LogInformation("Returning product models");

            return productModels!;
        }
    }
}
