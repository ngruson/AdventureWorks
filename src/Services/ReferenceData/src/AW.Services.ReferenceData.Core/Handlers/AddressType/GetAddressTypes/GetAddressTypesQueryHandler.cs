using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.Core.Handlers.AddressType.GetAddressTypes
{
    public class GetAddressTypesQueryHandler : IRequestHandler<GetAddressTypesQuery, List<AddressType>>
    {
        private readonly ILogger<GetAddressTypesQueryHandler> _logger;
        private readonly IRepository<Entities.AddressType> _repository;
        private readonly IMapper _mapper;

        public GetAddressTypesQueryHandler(
            ILogger<GetAddressTypesQueryHandler> logger,
            IRepository<Entities.AddressType> repository,
            IMapper mapper)
            => (_logger, _repository, _mapper) = (logger, repository, mapper);

        public async Task<List<AddressType>> Handle(GetAddressTypesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");

            _logger.LogInformation("Getting address types from database");
            var addressTypes = await _repository.ListAsync(cancellationToken);

            Guard.Against.Null(addressTypes, _logger);

            _logger.LogInformation("Returning address types");
            return _mapper.Map<List<AddressType>>(addressTypes);
        }
    }
}