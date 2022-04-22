using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.ReferenceData.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.Core.Handlers.Territory.GetTerritories
{
    public class GetTerritoriesQueryHandler : IRequestHandler<GetTerritoriesQuery, List<Territory>>
    {
        private readonly ILogger<GetTerritoriesQueryHandler> logger;
        private readonly IMapper mapper;
        private readonly IRepository<Entities.Territory> repository;

        public GetTerritoriesQueryHandler(
            ILogger<GetTerritoriesQueryHandler> logger,
            IRepository<Entities.Territory> repository,
            IMapper mapper) =>
                (this.logger, this.mapper, this.repository) = (logger, mapper, repository);

        public async Task<List<Territory>> Handle(GetTerritoriesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");
            List<Entities.Territory> territories;

            if (string.IsNullOrEmpty(request.CountryRegionCode))
            {
                logger.LogInformation("Getting territories from database");
                territories = await repository.ListAsync(cancellationToken);
            }
            else
            {
                logger.LogInformation("Getting territories for country {@Country} from database", request.CountryRegionCode);
                var spec = new GetTerritoriesForCountrySpecification(request.CountryRegionCode);
                territories = await repository.ListAsync(spec, cancellationToken);
            }
            
            Guard.Against.NullOrEmpty(territories, nameof(territories));

            logger.LogInformation("Returning territories");
            return mapper.Map<List<Territory>>(territories);
        }
    }
}