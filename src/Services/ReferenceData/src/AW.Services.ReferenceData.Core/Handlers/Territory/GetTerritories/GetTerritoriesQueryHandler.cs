using Ardalis.GuardClauses;
using Ardalis.Result;
using AutoMapper;
using AW.Services.ReferenceData.Core.GuardClauses;
using AW.Services.ReferenceData.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.ReferenceData.Core.Handlers.Territory.GetTerritories;

public class GetTerritoriesQueryHandler : IRequestHandler<GetTerritoriesQuery, Result<List<Territory>>>
{
    private readonly ILogger<GetTerritoriesQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<Entities.Territory> _repository;

    public GetTerritoriesQueryHandler(
        ILogger<GetTerritoriesQueryHandler> logger,
        IRepository<Entities.Territory> repository,
        IMapper mapper) =>
            (_logger, _mapper, _repository) = (logger, mapper, repository);

    public async Task<Result<List<Territory>>> Handle(GetTerritoriesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            List<Entities.Territory> territories;

            if (string.IsNullOrEmpty(request.CountryRegionCode))
            {
                _logger.LogInformation("Getting territories from database");
                territories = await _repository.ListAsync(cancellationToken);
            }
            else
            {
                _logger.LogInformation("Getting territories for country {@Country} from database", request.CountryRegionCode);
                var spec = new GetTerritoriesForCountrySpecification(request.CountryRegionCode);
                territories = await _repository.ListAsync(spec, cancellationToken);
            }

            var result = Guard.Against.TerritoriesNullOrEmpty(territories, _logger);
            if (!result.IsSuccess)
                return result;

            _logger.LogInformation("Returning territories");
            return _mapper.Map<List<Territory>>(territories).OrderBy(_ => _.Name).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred: {Message}", ex.Message);
            return Result.Error(ex.Message);
        }
    }
}
