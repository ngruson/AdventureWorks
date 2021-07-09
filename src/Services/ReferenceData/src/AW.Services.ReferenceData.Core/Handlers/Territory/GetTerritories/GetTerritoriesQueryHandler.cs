using Ardalis.GuardClauses;
using AutoMapper;
using AW.SharedKernel.Interfaces;
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

            logger.LogInformation("Getting territories from database");
            var territories = await repository.ListAsync();
            Guard.Against.NullOrEmpty(territories, nameof(territories));

            logger.LogInformation("Returning territories");
            return mapper.Map<List<Territory>>(territories);
        }
    }
}