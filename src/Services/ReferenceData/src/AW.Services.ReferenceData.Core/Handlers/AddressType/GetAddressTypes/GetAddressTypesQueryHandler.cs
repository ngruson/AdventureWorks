using Ardalis.GuardClauses;
using Ardalis.Result;
using AutoMapper;
using AW.Services.ReferenceData.Core.GuardClauses;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.ReferenceData.Core.Handlers.AddressType.GetAddressTypes;

public class GetAddressTypesQueryHandler : IRequestHandler<GetAddressTypesQuery, Result<List<AddressType>>>
{
    private readonly ILogger<GetAddressTypesQueryHandler> _logger;
    private readonly IRepository<Entities.AddressType> _repository;
    private readonly IMapper _mapper;

    public GetAddressTypesQueryHandler(
        ILogger<GetAddressTypesQueryHandler> logger,
        IRepository<Entities.AddressType> repository,
        IMapper mapper)
        => (_logger, _repository, _mapper) = (logger, repository, mapper);

    public async Task<Result<List<AddressType>>> Handle(GetAddressTypesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Getting address types from database");
            var addressTypes = await _repository.ListAsync(cancellationToken);

            var result = Guard.Against.AddressTypesNullOrEmpty(addressTypes, _logger);
            if (!result.IsSuccess)
                return result;

            _logger.LogInformation("Returning address types");
            return _mapper.Map<List<AddressType>>(addressTypes);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred: {Message}", ex.Message);
            return Result.Error(ex.Message);
        }
    }
}
