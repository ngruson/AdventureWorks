using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Customer.Core.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Customer.Core.Handlers.GetPreferredAddress
{
    public class GetPreferredAddressQueryHandler : IRequestHandler<GetPreferredAddressQuery, AddressDto?>
    {
        private readonly ILogger<GetPreferredAddressQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.Customer> _repository;

        public GetPreferredAddressQueryHandler(
            ILogger<GetPreferredAddressQueryHandler> logger,
            IMapper mapper,
            IRepository<Entities.Customer> repository
        ) => 
            (_logger, _mapper, _repository) = (logger, mapper, repository);

        public async Task<AddressDto?> Handle(GetPreferredAddressQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Getting preferred {AddressType} address for customer {AccountNumber} from database",
                request.AddressType, request.AccountNumber
            );

            var customer = await _repository.SingleOrDefaultAsync(                
                new GetCustomerAddressesSpecification(request.AccountNumber),
                cancellationToken
            );
            Guard.Against.CustomerNull(customer, request.AccountNumber, _logger);

            var address = customer!.GetPreferredAddress(request.AddressType);

            _logger.LogInformation("Returning address");
            return _mapper.Map<AddressDto>(address);
        }
    }
}