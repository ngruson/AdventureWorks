using Ardalis.GuardClauses;
using Ardalis.Specification;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.Application.Territory.GetTerritories
{
    public class GetTerritoriesQueryHandler : IRequestHandler<GetTerritoriesQuery, List<Territory>>
    {
        private readonly ILogger<GetTerritoriesQueryHandler> logger;
        private readonly IMapper mapper;
        private readonly IRepositoryBase<Domain.Territory> repository;

        public GetTerritoriesQueryHandler(
            ILogger<GetTerritoriesQueryHandler> logger,
            IRepositoryBase<Domain.Territory> repository,
            IMapper mapper) =>
                (this.logger, this.mapper, this.repository) = (logger, mapper, repository);

        public async Task<List<Territory>> Handle(GetTerritoriesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");

            logger.LogInformation("Getting territories from database");
            var territories = await repository.ListAsync();
            Guard.Against.NullOrEmpty(territories, nameof(territories));

            logger.LogInformation("Returning territories");
            return mapper.Map<List<Territory>>(territories);
        }
    }
}