using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using System.Threading;
using Microsoft.Extensions.Logging;
using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.SharedKernel.Interfaces;
using AW.Services.ReferenceData.Core.GuardClauses;

namespace AW.Services.ReferenceData.Core.Handlers.CountryRegion.GetCountries
{
    public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, List<Country>>
    {
        private readonly ILogger<GetCountriesQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.CountryRegion> _repository;

        public GetCountriesQueryHandler(
            ILogger<GetCountriesQueryHandler> logger,
            IRepository<Entities.CountryRegion> repository,
            IMapper mapper) =>
                (_logger, _mapper, _repository) = (logger, mapper, repository);

        public async Task<List<Country>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");

            _logger.LogInformation("Getting countries from database");
            var countries = await _repository.ListAsync(cancellationToken);

            Guard.Against.CountriesNullOrEmpty(countries, _logger);

            _logger.LogInformation("Returning countries");
            return _mapper.Map<List<Country>>(countries);
        }
    }
}