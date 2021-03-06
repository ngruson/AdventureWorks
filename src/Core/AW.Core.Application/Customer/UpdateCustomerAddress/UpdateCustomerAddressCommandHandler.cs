﻿using Ardalis.Specification;
using AutoMapper;
using AW.Core.Application.Specifications;
using AW.Core.Domain.Person;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Core.Application.Customer.UpdateCustomerAddress
{
    public class UpdateCustomerAddressCommandHandler : IRequestHandler<UpdateCustomerAddressCommand, Unit>
    {
        private readonly IMapper mapper;
        private readonly IRepositoryBase<Address> addressRepository;
        private readonly IRepositoryBase<Domain.Sales.Customer> customerRepository;
        private readonly IRepositoryBase<Domain.Person.StateProvince> stateProvinceRepository;

        public UpdateCustomerAddressCommandHandler(
            IMapper mapper,
            IRepositoryBase<Address> addressRepository,
            IRepositoryBase<Domain.Person.AddressType> addressTypeRepository,
            IRepositoryBase<Domain.Sales.Customer> customerRepository,
            IRepositoryBase<Domain.Person.StateProvince> stateProvinceRepository
        ) => (this.mapper, this.addressRepository, this.customerRepository, this.stateProvinceRepository) =
                (mapper, addressRepository, customerRepository, stateProvinceRepository);

        public async Task<Unit> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetBySpecAsync(
                new GetCustomerSpecification(request.AccountNumber)
            );

            var stateProvince = await stateProvinceRepository.GetBySpecAsync(
                new GetStateProvinceSpecification(request.CustomerAddress.Address.StateProvinceCode)
            );

            BusinessEntityAddress customerAddress = null;
            if (customer.Store != null)
                customerAddress = customer.Store.BusinessEntityAddresses.FirstOrDefault(
                    a => a.AddressType.Name == request.CustomerAddress.AddressTypeName
                );
            else if (customer.Person != null)
                customerAddress = customer.Person.BusinessEntityAddresses.FirstOrDefault(
                    a => a.AddressType.Name == request.CustomerAddress.AddressTypeName
                );

            if (customerAddress != null)
            {
                var existingAddressID = await IsExistingAddress(request.CustomerAddress.Address, stateProvince.Id);

                if (existingAddressID.HasValue)
                    customerAddress.AddressID = existingAddressID.Value;
                else
                {
                    customerAddress.Address = mapper.Map<Address>(request.CustomerAddress.Address);
                    customerAddress.Address.StateProvinceID = stateProvince.Id;
                }

                customerAddress.ModifiedDate = DateTime.Now;
                customerAddress.rowguid = Guid.NewGuid();

                await customerRepository.UpdateAsync(customer);
            }

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