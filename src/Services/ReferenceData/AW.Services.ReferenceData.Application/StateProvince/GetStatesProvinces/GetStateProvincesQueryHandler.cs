using Ardalis.GuardClauses;
using Ardalis.Specification;
using AutoMapper;
using AW.Services.ReferenceData.Application.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.Application.StateProvince.GetStatesProvinces
{
    public class GetStatesProvincesQueryHandler : IRequestHandler<GetStatesProvincesQuery, List<StateProvince>>
    {
        private readonly ILogger<GetStatesProvincesQueryHandler> logger;
        private readonly IMapper mapper;
        private readonly IRepositoryBase<Domain.StateProvince> repository;

        public GetStatesProvincesQueryHandler(
            ILogger<GetStatesProvincesQueryHandler> logger,
            IRepositoryBase<Domain.StateProvince> repository,
            IMapper mapper) =>
                (this.logger, this.mapper, this.repository) = (logger, mapper, repository);

        public async Task<List<StateProvince>> Handle(GetStatesProvincesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");

            List<Domain.StateProvince> statesProvinces;
            if (string.IsNullOrEmpty(request.CountryRegionCode))
            {
                logger.LogInformation("Getting all state/provinces from database");
                statesProvinces = await repository.ListAsync();
            }
            else
            {
                logger.LogInformation("Getting all state/provinces for country {@Country} from database", request.CountryRegionCode);
                var spec = new GetStatesProvincesForCountrySpecification(request.CountryRegionCode);
                statesProvinces = await repository.ListAsync(spec);
            }
            
            Guard.Against.NullOrEmpty(statesProvinces, nameof(statesProvinces));

            logger.LogInformation("Returning state/provinces");
            return mapper.Map<List<StateProvince>>(statesProvinces);
        }
    }
}