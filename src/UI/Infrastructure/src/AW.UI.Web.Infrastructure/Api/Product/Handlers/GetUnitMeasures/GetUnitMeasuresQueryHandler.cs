using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.Product.Handlers.GetUnitMeasures
{
    public class GetUnitMeasuresQueryHandler : IRequestHandler<GetUnitMeasuresQuery, List<UnitMeasure>>
    {
        private readonly ILogger<GetUnitMeasuresQueryHandler> _logger;
        private readonly IProductApiClient _client;

        public GetUnitMeasuresQueryHandler(ILogger<GetUnitMeasuresQueryHandler> logger, IProductApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<List<UnitMeasure>> Handle(GetUnitMeasuresQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting unit measures from API");

            var unitMeasures = await _client.GetUnitMeasures();
            Guard.Against.Null(unitMeasures, _logger);

            _logger.LogInformation("Returning unit measures");

            return unitMeasures!;
        }
    }
}
