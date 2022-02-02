using Ardalis.GuardClauses;
using AutoMapper;
using AW.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.Core.Handlers.ContactType.GetContactTypes
{
    public class GetContactTypesQueryHandler : IRequestHandler<GetContactTypesQuery, List<ContactType>>
    {
        private readonly ILogger<GetContactTypesQueryHandler> logger;
        private readonly IRepository<Entities.ContactType> repository;
        private readonly IMapper mapper;

        public GetContactTypesQueryHandler(
            ILogger<GetContactTypesQueryHandler> logger,
            IRepository<Entities.ContactType> repository,
            IMapper mapper)
            => (this.logger, this.repository, this.mapper) = (logger, repository, mapper);

        public async Task<List<ContactType>> Handle(GetContactTypesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");

            logger.LogInformation("Getting contact types from database");
            var contactTypes = await repository.ListAsync(cancellationToken);

            Guard.Against.Null(contactTypes, nameof(contactTypes));

            logger.LogInformation("Returning contact types");
            return mapper.Map<List<ContactType>>(contactTypes);
        }
    }
}