using Ardalis.GuardClauses;
using Ardalis.Result;
using AutoMapper;
using AW.Services.ReferenceData.Core.GuardClauses;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.ReferenceData.Core.Handlers.ShipMethod.GetShipMethods;

public class GetShipMethodsQueryHandler : IRequestHandler<GetShipMethodsQuery, Result<List<ShipMethod>>>
{
    private readonly ILogger<GetShipMethodsQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<Entities.ShipMethod> _repository;

    public GetShipMethodsQueryHandler(
        ILogger<GetShipMethodsQueryHandler> logger,
        IRepository<Entities.ShipMethod> repository,
        IMapper mapper
    ) =>
        (_logger, _mapper, _repository) = (logger, mapper, repository);

    public async Task<Result<List<ShipMethod>>> Handle(GetShipMethodsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Getting shipping methods from database");
            var shipMethods = await _repository.ListAsync(cancellationToken);
            
            var result = Guard.Against.ShipMethodsNullOrEmpty(shipMethods, _logger);
            if (!result.IsSuccess)
                return result;

            _logger.LogInformation("Returning shipping methods");
            return _mapper.Map<List<ShipMethod>>(shipMethods).OrderBy(_ => _.Name).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred: {Message}", ex.Message);
            return Result.Error(ex.Message);
        }
    }
}
