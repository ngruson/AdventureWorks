using Ardalis.GuardClauses;
using AutoMapper;
using AW.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.Core.Handlers.AddressType.GetAddressTypes
{
    public class GetAddressTypesQueryHandler : IRequestHandler<GetAddressTypesQuery, List<AddressType>>
    {
        private readonly ILogger<GetAddressTypesQueryHandler> logger;
        private readonly IRepository<Entities.AddressType> repository;
        private readonly IMapper mapper;

        public GetAddressTypesQueryHandler(
            ILogger<GetAddressTypesQueryHandler> logger,
            IRepository<Entities.AddressType> repository,
            IMapper mapper)
            => (this.logger, this.repository, this.mapper) = (logger, repository, mapper);

        public async Task<List<AddressType>> Handle(GetAddressTypesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");

            logger.LogInformation("Getting address types from database");
            var addressTypes = await repository.ListAsync();

            Guard.Against.Null(addressTypes, nameof(addressTypes));

            logger.LogInformation("Returning address types");
            return mapper.Map<List<AddressType>>(addressTypes);
        }
    }
}