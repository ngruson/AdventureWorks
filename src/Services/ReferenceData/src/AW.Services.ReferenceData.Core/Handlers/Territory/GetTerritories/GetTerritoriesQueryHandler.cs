using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.ReferenceData.Core.Exceptions;
using AW.Services.ReferenceData.Core.GuardClauses;
using AW.Services.ReferenceData.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.ReferenceData.Core.Handlers.Territory.GetTerritories
{
    public class GetTerritoriesQueryHandler : IRequestHandler<GetTerritoriesQuery, List<Territory>>
    {
        private readonly ILogger<GetTerritoriesQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.Territory> _repository;

        public GetTerritoriesQueryHandler(
            ILogger<GetTerritoriesQueryHandler> logger,
            IRepository<Entities.Territory> repository,
            IMapper mapper) =>
                (_logger, _mapper, _repository) = (logger, mapper, repository);

        public async Task<List<Territory>> Handle(GetTerritoriesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");
            List<Entities.Territory> territories;

            if (string.IsNullOrEmpty(request.CountryRegionCode))
            {
                _logger.LogInformation("Getting territories from database");
                territories = await _repository.ListAsync(cancellationToken);
            }
            else
            {
                _logger.LogInformation("Getting territories for country {@Country} from database", request.CountryRegionCode);
                var spec = new GetTerritoriesForCountrySpecification(request.CountryRegionCode);
                territories = await _repository.ListAsync(spec, cancellationToken);
            }
            
            Guard.Against.TerritoriesNullOrEmpty(territories, _logger);

            _logger.LogInformation("Returning territories");
            return _mapper.Map<List<Territory>>(territories).OrderBy(_ => _.Name).ToList();
        }
    }
}