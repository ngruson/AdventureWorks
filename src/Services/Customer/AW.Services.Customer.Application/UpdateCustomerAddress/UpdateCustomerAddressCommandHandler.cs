using Ardalis.GuardClauses;
using Ardalis.Specification;
using AutoMapper;
using AW.Services.Customer.Application.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Application.UpdateCustomerAddress
{
    public class UpdateCustomerAddressCommandHandler : IRequestHandler<UpdateCustomerAddressCommand, Unit>
    {
        private readonly ILogger<UpdateCustomerAddressCommandHandler> logger;
        private readonly IMapper mapper;        
        private readonly IRepositoryBase<Domain.Customer> customerRepository;
        private readonly IRepositoryBase<Domain.Address> addressRepository;

        public UpdateCustomerAddressCommandHandler(
            ILogger<UpdateCustomerAddressCommandHandler> logger,
            IMapper mapper,            
            IRepositoryBase<Domain.Customer> customerRepository,
            IRepositoryBase<Domain.Address> addressRepository
        ) => (this.logger, this.mapper, this.customerRepository, this.addressRepository) =
                (logger, mapper, customerRepository, addressRepository);

        public async Task<Unit> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");
            logger.LogInformation("Getting customer from database");

            var customer = await customerRepository.GetBySpecAsync(
                new GetCustomerSpecification(request.AccountNumber)
            );
            Guard.Against.Null(customer, nameof(customer));

            logger.LogInformation("Getting address from database");
            var customerAddress = customer.Addresses.FirstOrDefault(
                ca => ca.AddressType == request.CustomerAddress.AddressType
            );
            Guard.Against.Null(customerAddress, nameof(customerAddress));


            var existingAddress = await IsExistingAddress(request.CustomerAddress.Address);

            if (existingAddress != null)
            {
                logger.LogInformation("Found existing address");
                customerAddress.AddressID = existingAddress.Id;
            }
            else
            {
                logger.LogInformation("Add new address");
                customerAddress.Address = mapper.Map<Domain.Address>(request.CustomerAddress.Address);
            }

            logger.LogInformation("Saving customer to database");
            await customerRepository.UpdateAsync(customer);

            return Unit.Value;
        }

        private async Task<Domain.Address> IsExistingAddress(AddressDto addressDto)
        {
            var address = await addressRepository.GetBySpecAsync(
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