using Ardalis.GuardClauses;
using Ardalis.Specification;
using AutoMapper;
using AW.Services.ReferenceData.Application.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.Application.StateProvince.GetStateProvinces
{
    public class GetStateProvincesQueryHandler : IRequestHandler<GetStateProvincesQuery, List<StateProvince>>
    {
        private readonly ILogger<GetStateProvincesQueryHandler> logger;
        private readonly IMapper mapper;
        private readonly IRepositoryBase<Domain.StateProvince> repository;

        public GetStateProvincesQueryHandler(
            ILogger<GetStateProvincesQueryHandler> logger,
            IRepositoryBase<Domain.StateProvince> repository,
            IMapper mapper) =>
                (this.logger, this.mapper, this.repository) = (logger, mapper, repository);

        public async Task<List<StateProvince>> Handle(GetStateProvincesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");

            List<Domain.StateProvince> stateProvinces;
            if (string.IsNullOrEmpty(request.CountryRegionCode))
            {
                logger.LogInformation("Getting all state/provinces from database");
                stateProvinces = await repository.ListAsync();
            }
            else
            {
                logger.LogInformation("Getting all state/provinces for country {@Country} from database", request.CountryRegionCode);
                var spec = new GetStateProvincesForCountrySpecification(request.CountryRegionCode);
                stateProvinces = await repository.ListAsync(spec);
            }
            
            Guard.Against.NullOrEmpty(stateProvinces, nameof(stateProvinces));

            logger.LogInformation("Returning state/provinces");
            return mapper.Map<List<StateProvince>>(stateProvinces);
        }
    }
}