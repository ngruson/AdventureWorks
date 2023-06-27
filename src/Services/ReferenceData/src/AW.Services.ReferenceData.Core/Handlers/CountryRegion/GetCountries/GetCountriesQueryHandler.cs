using MediatR;
using Microsoft.Extensions.Logging;
using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.SharedKernel.Interfaces;
using AW.Services.ReferenceData.Core.GuardClauses;
using Ardalis.Result;

namespace AW.Services.ReferenceData.Core.Handlers.CountryRegion.GetCountries;

public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, Result<List<Country>>>
{
    private readonly ILogger<GetCountriesQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<Entities.CountryRegion> _repository;

    public GetCountriesQueryHandler(
        ILogger<GetCountriesQueryHandler> logger,
        IRepository<Entities.CountryRegion> repository,
        IMapper mapper) =>
            (_logger, _mapper, _repository) = (logger, mapper, repository);

    public async Task<Result<List<Country>>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Getting countries from database");
            var countries = await _repository.ListAsync(cancellationToken);

            var result = Guard.Against.CountriesNullOrEmpty(countries, _logger);
            if (!result.IsSuccess)
                return result;

            _logger.LogInformation("Returning countries");
            return _mapper.Map<List<Country>>(countries);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred: {Message}", ex.Message);
            return Result.Error(ex.Message);
        }
    }
}
