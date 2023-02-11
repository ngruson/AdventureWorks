using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Customer.Core.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Customer.Core.Handlers.UpdateCustomerAddress
{
    public class UpdateCustomerAddressCommandHandler : IRequestHandler<UpdateCustomerAddressCommand, Unit>
    {
        private readonly ILogger<UpdateCustomerAddressCommandHandler> _logger;
        private readonly IMapper _mapper;        
        private readonly IRepository<Entities.Customer> _customerRepository;
        private readonly IRepository<Entities.Address> _addressRepository;

        public UpdateCustomerAddressCommandHandler(
            ILogger<UpdateCustomerAddressCommandHandler> logger,
            IMapper mapper,            
            IRepository<Entities.Customer> customerRepository,
            IRepository<Entities.Address> addressRepository
        ) => (_logger, _mapper, _customerRepository, _addressRepository) =
                (logger, mapper, customerRepository, addressRepository);

        public async Task<Unit> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");
            _logger.LogInformation("Getting customer from database");

            var customer = await _customerRepository.SingleOrDefaultAsync(
                new GetCustomerSpecification(request.AccountNumber),
                cancellationToken
            );
            Guard.Against.CustomerNull(customer, request.AccountNumber, _logger);

            _logger.LogInformation("Getting address from database");
            var customerAddress = customer!.Addresses.FirstOrDefault(
                ca => ca.AddressType == request.CustomerAddress?.AddressType
            );
            Guard.Against.AddressNull(
                customerAddress, 
                request.AccountNumber, 
                request.CustomerAddress!.AddressType!,
                _logger
            );

            var existingAddress = await IsExistingAddress(request.CustomerAddress.Address!);

            if (existingAddress != null)
            {
                _logger.LogInformation("Found existing address");
                customerAddress!.Address = existingAddress;
            }
            else
            {
                _logger.LogInformation("Add new address");
                customerAddress!.Address = _mapper.Map<Entities.Address>(request.CustomerAddress.Address);
            }

            _logger.LogInformation("Saving customer to database");
            await _customerRepository.UpdateAsync(customer, cancellationToken);

            return Unit.Value;
        }

        private async Task<Entities.Address?> IsExistingAddress(AddressDto addressDto)
        {
            var address = await _addressRepository.SingleOrDefaultAsync(
                new GetAddressSpecification(
                    addressDto.AddressLine1!,
                    addressDto.AddressLine2!,
                    addressDto.PostalCode!,
                    addressDto.City!,
                    addressDto.StateProvinceCode!,
                    addressDto.CountryRegionCode!
                )
            );

            if (address != null)
                return address;

            return null;
        }
    }
}
