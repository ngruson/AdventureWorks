using AutoMapper;
using AW.Application.Interfaces;
using AW.Application.Specifications;
using AW.Domain.Person;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.Customer.AddCustomerAddress
{
    public class AddCustomerAddressCommandHandler : IRequestHandler<AddCustomerAddressCommand, Unit>
    {
        private readonly IMapper mapper;
        private readonly IAsyncRepository<Domain.Person.Address> addressRepository;
        private readonly IAsyncRepository<Domain.Person.AddressType> addressTypeRepository;
        private readonly IAsyncRepository<Domain.Sales.Customer> customerRepository;
        private readonly IAsyncRepository<Domain.Person.StateProvince> stateProvinceRepository;

        public AddCustomerAddressCommandHandler(
            IMapper mapper,
            IAsyncRepository<Domain.Person.Address> addressRepository,
            IAsyncRepository<Domain.Person.AddressType> addressTypeRepository,
            IAsyncRepository<Domain.Sales.Customer> customerRepository,
            IAsyncRepository<Domain.Person.StateProvince> stateProvinceRepository
        ) => (this.mapper, this.addressRepository, this.addressTypeRepository, this.customerRepository, this.stateProvinceRepository) = 
                (mapper, addressRepository, addressTypeRepository, customerRepository, stateProvinceRepository);
        
        public async Task<Unit> Handle(AddCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.FirstOrDefaultAsync(
                new GetCustomerSpecification(request.AccountNumber)
            );

            var addressType = await addressTypeRepository.FirstOrDefaultAsync(
                new GetAddressTypeSpecification(request.CustomerAddress.AddressTypeName)
            );
            var stateProvince = await stateProvinceRepository.FirstOrDefaultAsync(
                new GetStateProvinceSpecification(request.CustomerAddress.Address.StateProvinceCode)
            );

            var customerAddress = new BusinessEntityAddress
            {
                AddressTypeID = addressType.AddressTypeID,
                ModifiedDate = DateTime.Now,
                rowguid = Guid.NewGuid()
            };

            var existingAddressID = await IsExistingAddress(request.CustomerAddress.Address, stateProvince.Id);

            if (existingAddressID.HasValue)
                customerAddress.AddressID = existingAddressID.Value;
            else
            {
                customerAddress.Address = mapper.Map<Address>(request.CustomerAddress.Address);
                customerAddress.Address.StateProvinceID = stateProvince.Id;
            }

            if (customer.Store != null)
                customer.Store.BusinessEntityAddresses.Add(customerAddress);
            else if (customer.Person != null)
                customer.Person.BusinessEntityAddresses.Add(customerAddress);

            await customerRepository.UpdateAsync(customer);

            return Unit.Value;
        }

        private async Task<int?> IsExistingAddress(AddressDto addressDto, int stateProvinceID)
        {
            var address = await addressRepository.FirstOrDefaultAsync(
                new GetAddressSpecification(
                    addressDto.AddressLine1,
                    addressDto.AddressLine2,
                    addressDto.City,
                    stateProvinceID,
                    addressDto.PostalCode
                )
            );

            if (address != null)
                return address.Id;

            return null;            
        }
    }
}