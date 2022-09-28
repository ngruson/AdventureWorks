using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.Core.Handlers.ContactType.GetContactTypes
{
    public class GetContactTypesQueryHandler : IRequestHandler<GetContactTypesQuery, List<ContactType>>
    {
        private readonly ILogger<GetContactTypesQueryHandler> _logger;
        private readonly IRepository<Entities.ContactType> _repository;
        private readonly IMapper _mapper;

        public GetContactTypesQueryHandler(
            ILogger<GetContactTypesQueryHandler> logger,
            IRepository<Entities.ContactType> repository,
            IMapper mapper)
            => (_logger, _repository, _mapper) = (logger, repository, mapper);

        public async Task<List<ContactType>> Handle(GetContactTypesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");

            _logger.LogInformation("Getting contact types from database");
            var contactTypes = await _repository.ListAsync(cancellationToken);

            Guard.Against.Null(contactTypes, _logger);

            _logger.LogInformation("Returning contact types");
            return _mapper.Map<List<ContactType>>(contactTypes);
        }
    }
}