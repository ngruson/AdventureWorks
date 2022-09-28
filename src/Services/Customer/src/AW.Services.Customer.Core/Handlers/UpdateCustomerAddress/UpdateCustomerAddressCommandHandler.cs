using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Core.Handlers.UpdateCustomerAddress
{
    public class UpdateCustomerAddressCommandHandler : IRequestHandler<UpdateCustomerAddressCommand, Unit>
    {
        private readonly ILogger<UpdateCustomerAddressCommandHandler> logger;
        private readonly IMapper mapper;        
        private readonly IRepository<Entities.Customer> customerRepository;
        private readonly IRepository<Entities.Address> addressRepository;

        public UpdateCustomerAddressCommandHandler(
            ILogger<UpdateCustomerAddressCommandHandler> logger,
            IMapper mapper,            
            IRepository<Entities.Customer> customerRepository,
            IRepository<Entities.Address> addressRepository
        ) => (this.logger, this.mapper, this.customerRepository, this.addressRepository) =
                (logger, mapper, customerRepository, addressRepository);

        public async Task<Unit> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");
            logger.LogInformation("Getting customer from database");

            var customer = await customerRepository.SingleOrDefaultAsync(
                new GetCustomerSpecification(request.AccountNumber),
                cancellationToken
            );
            Guard.Against.Null(customer, nameof(customer));

            logger.LogInformation("Getting address from database");
            var customerAddress = customer.Addresses.FirstOrDefault(
                ca => ca.AddressType == request.CustomerAddress.AddressType
            );
            Guard.Against.Null(customerAddress, logger);


            var existingAddress = await IsExistingAddress(request.CustomerAddress.Address);

            if (existingAddress != null)
            {
                logger.LogInformation("Found existing address");
                customerAddress.Address = existingAddress;
            }
            else
            {
                logger.LogInformation("Add new address");
                customerAddress.Address = mapper.Map<Entities.Address>(request.CustomerAddress.Address);
            }

            logger.LogInformation("Saving customer to database");
            await customerRepository.UpdateAsync(customer, cancellationToken);

            return Unit.Value;
        }

        private async Task<Entities.Address> IsExistingAddress(AddressDto addressDto)
        {
            var address = await addressRepository.SingleOrDefaultAsync(
                new GetAddressSpecification(
                    addressDto.AddressLine1,
                    addressDto.AddressLine2,
                    addressDto.PostalCode,
                    addressDto.City,
                    addressDto.StateProvinceCode,
                    addressDto.CountryRegionCode
                )
            );

            if (address != null)
                return address;

            return null;
        }
    }
}