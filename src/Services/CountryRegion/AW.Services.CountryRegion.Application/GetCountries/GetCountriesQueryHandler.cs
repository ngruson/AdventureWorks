using Ardalis.Specification;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using System.Threading;
using Microsoft.Extensions.Logging;
using Ardalis.GuardClauses;
using AutoMapper;

namespace AW.Services.CountryRegion.Application.GetCountries
{
    public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, List<CountryDto>>
    {
        private readonly ILogger<GetCountriesQueryHandler> logger;
        private readonly IMapper mapper;
        private readonly IRepositoryBase<Domain.CountryRegion> repository;

        public GetCountriesQueryHandler(
            ILogger<GetCountriesQueryHandler> logger,
            IRepositoryBase<Domain.CountryRegion> repository,
            IMapper mapper) =>
                (this.logger, this.mapper, this.repository) = (logger, mapper, repository);

        public async Task<List<CountryDto>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");

            logger.LogInformation("Getting countries from database");
            var countries = await repository.ListAsync();

            Guard.Against.NullOrEmpty(countries, nameof(countries));

            logger.LogInformation("Returning countries");
            return mapper.Map<List<CountryDto>>(countries);
        }
    }
}