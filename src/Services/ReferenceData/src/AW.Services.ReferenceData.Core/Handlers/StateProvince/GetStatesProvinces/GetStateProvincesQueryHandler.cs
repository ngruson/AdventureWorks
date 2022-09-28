using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.ReferenceData.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.Core.Handlers.StateProvince.GetStatesProvinces
{
    public class GetStatesProvincesQueryHandler : IRequestHandler<GetStatesProvincesQuery, List<StateProvince>>
    {
        private readonly ILogger<GetStatesProvincesQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.StateProvince> _repository;

        public GetStatesProvincesQueryHandler(
            ILogger<GetStatesProvincesQueryHandler> logger,
            IRepository<Entities.StateProvince> repository,
            IMapper mapper) =>
                (_logger, _mapper, _repository) = (logger, mapper, repository);

        public async Task<List<StateProvince>> Handle(GetStatesProvincesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");

            List<Entities.StateProvince> statesProvinces;
            if (string.IsNullOrEmpty(request.CountryRegionCode))
            {
                _logger.LogInformation("Getting all state/provinces from database");
                var spec = new GetStatesProvincesSpecification();
                statesProvinces = await _repository.ListAsync(spec, cancellationToken);
            }
            else
            {
                _logger.LogInformation("Getting all state/provinces for country {@Country} from database", request.CountryRegionCode);
                var spec = new GetStatesProvincesSpecification(request.CountryRegionCode);
                statesProvinces = await _repository.ListAsync(spec, cancellationToken);
            }
            
            Guard.Against.Null(statesProvinces, _logger);

            _logger.LogInformation("Returning state/provinces");
            return _mapper.Map<List<StateProvince>>(statesProvinces);
        }
    }
}