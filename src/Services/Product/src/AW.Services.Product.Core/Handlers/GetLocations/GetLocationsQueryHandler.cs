using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Product.Core.GuardClauses;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Product.Core.Handlers.GetLocations
{
    public class GetLocationsQueryHandler : IRequestHandler<GetLocationsQuery, List<Location>>
    {
        private readonly ILogger<GetLocationsQueryHandler> _logger;
        private readonly IRepository<Entities.Location> _repository;
        private readonly IMapper _mapper;

        public GetLocationsQueryHandler(
            ILogger<GetLocationsQueryHandler> logger,
            IRepository<Entities.Location> repository,
            IMapper mapper)
            => (_logger, _repository, _mapper) = (logger, repository, mapper);

        public async Task<List<Location>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting locations from database");

            var spec = new GetLocationsSpecification();
            var locations = await _repository.ListAsync(spec, cancellationToken);
            Guard.Against.LocationsNullOrEmpty(locations, _logger);

            _logger.LogInformation("Returning locations");
            return _mapper.Map<List<Location>>(locations);
        }
    }
}
