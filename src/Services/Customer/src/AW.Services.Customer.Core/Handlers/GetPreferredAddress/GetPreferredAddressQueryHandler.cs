using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Customer.Core.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Core.Handlers.GetPreferredAddress
{
    public class GetPreferredAddressQueryHandler : IRequestHandler<GetPreferredAddressQuery, AddressDto>
    {
        private readonly ILogger<GetPreferredAddressQueryHandler> logger;
        private readonly IMapper mapper;
        private readonly IRepository<Entities.Customer> repository;

        public GetPreferredAddressQueryHandler(
            ILogger<GetPreferredAddressQueryHandler> logger,
            IMapper mapper,
            IRepository<Entities.Customer> repository
        ) => 
            (this.logger, this.mapper, this.repository) = (logger, mapper, repository);

        public async Task<AddressDto> Handle(GetPreferredAddressQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "Getting preferred {AddressType} address for customer {AccountNumber} from database",
                request.AddressType, request.AccountNumber
            );

            var customer = await repository.GetBySpecAsync(                
                new GetCustomerAddressesSpecification(request.AccountNumber),
                cancellationToken
            );
            Guard.Against.CustomerNull(customer, request.AccountNumber);

            var address = customer.GetPreferredAddress(request.AddressType);

            logger.LogInformation("Returning address");
            return mapper.Map<AddressDto>(address);
        }
    }
}