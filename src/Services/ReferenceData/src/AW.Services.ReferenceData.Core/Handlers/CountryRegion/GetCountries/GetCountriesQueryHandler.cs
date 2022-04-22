using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using System.Threading;
using Microsoft.Extensions.Logging;
using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.SharedKernel.Interfaces;

namespace AW.Services.ReferenceData.Core.Handlers.CountryRegion.GetCountries
{
    public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, List<Country>>
    {
        private readonly ILogger<GetCountriesQueryHandler> logger;
        private readonly IMapper mapper;
        private readonly IRepository<Entities.CountryRegion> repository;

        public GetCountriesQueryHandler(
            ILogger<GetCountriesQueryHandler> logger,
            IRepository<Entities.CountryRegion> repository,
            IMapper mapper) =>
                (this.logger, this.mapper, this.repository) = (logger, mapper, repository);

        public async Task<List<Country>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");

            logger.LogInformation("Getting countries from database");
            var countries = await repository.ListAsync(cancellationToken);

            Guard.Against.NullOrEmpty(countries, nameof(countries));

            logger.LogInformation("Returning countries");
            return mapper.Map<List<Country>>(countries);
        }
    }
}