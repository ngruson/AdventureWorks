using Ardalis.GuardClauses;
using Ardalis.Result;
using AutoMapper;
using AW.Services.ReferenceData.Core.GuardClauses;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.ReferenceData.Core.Handlers.ContactType.GetContactTypes;

public class GetContactTypesQueryHandler : IRequestHandler<GetContactTypesQuery, Result<List<ContactType>>>
{
    private readonly ILogger<GetContactTypesQueryHandler> _logger;
    private readonly IRepository<Entities.ContactType> _repository;
    private readonly IMapper _mapper;

    public GetContactTypesQueryHandler(
        ILogger<GetContactTypesQueryHandler> logger,
        IRepository<Entities.ContactType> repository,
        IMapper mapper)
        => (_logger, _repository, _mapper) = (logger, repository, mapper);

    public async Task<Result<List<ContactType>>> Handle(GetContactTypesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Getting contact types from database");
            var contactTypes = await _repository.ListAsync(cancellationToken);

            var result = Guard.Against.ContactTypesNullOrEmpty(contactTypes, _logger);
            if (!result.IsSuccess)
                return result;

            _logger.LogInformation("Returning contact types");
            return _mapper.Map<List<ContactType>>(contactTypes);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred: {Message}", ex.Message);
            return Result.Error(ex.Message);
        }
        
    }
}
