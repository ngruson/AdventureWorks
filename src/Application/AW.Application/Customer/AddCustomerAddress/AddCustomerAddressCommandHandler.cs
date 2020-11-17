using Ardalis.Specification;
using AutoMapper;
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
        private readonly IRepositoryBase<Address> addressRepository;
        private readonly IRepositoryBase<Domain.Person.AddressType> addressTypeRepository;
        private readonly IRepositoryBase<Domain.Sales.Customer> customerRepository;
        private readonly IRepositoryBase<Domain.Person.StateProvince> stateProvinceRepository;

        public AddCustomerAddressCommandHandler(
            IMapper mapper,
            IRepositoryBase<Address> addressRepository,
            IRepositoryBase<Domain.Person.AddressType> addressTypeRepository,
            IRepositoryBase<Domain.Sales.Customer> customerRepository,
            IRepositoryBase<Domain.Person.StateProvince> stateProvinceRepository
        ) => (this.mapper, this.addressRepository, this.addressTypeRepository, this.customerRepository, this.stateProvinceRepository) = 
                (mapper, addressRepository, addressTypeRepository, customerRepository, stateProvinceRepository);
        
        public async Task<Unit> Handle(AddCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetBySpecAsync(
                new GetCustomerSpecification(request.AccountNumber)
            );

            var addressType = await addressTypeRepository.GetBySpecAsync(
                new GetAddressTypeSpecification(request.CustomerAddress.AddressTypeName)
            );
            var stateProvince = await stateProvinceRepository.GetBySpecAsync(
                new GetStateProvinceSpecification(request.CustomerAddress.Address.StateProvinceCode)
            );

            var customerAddress = new BusinessEntityAddress
            {
                AddressTypeID = addressType.Id,
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
            var address = await addressRepository.GetBySpecAsync(
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