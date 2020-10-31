using AW.Application.Interfaces;
using AW.Application.Specifications;
using AW.Domain.Person;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.Customer.UpdateCustomerAddress
{
    public class UpdateCustomerContactCommandValidator : AbstractValidator<UpdateCustomerAddressCommand>
    {
        private readonly IAsyncRepository<Domain.Sales.Customer> customerRepository;
        private readonly IAsyncRepository<Domain.Person.AddressType> addressTypeRepository;
        private readonly IAsyncRepository<Domain.Person.StateProvince> stateProvinceRepository;

        public UpdateCustomerContactCommandValidator(
            IAsyncRepository<Domain.Sales.Customer> customerRepository,
            IAsyncRepository<Domain.Person.AddressType> addressTypeRepository,
            IAsyncRepository<Domain.Person.StateProvince> stateProvinceRepository
        )
        {
            this.customerRepository = customerRepository;
            this.addressTypeRepository = addressTypeRepository;
            this.stateProvinceRepository = stateProvinceRepository;

            RuleFor(cmd => cmd.AccountNumber)
                .NotEmpty().WithMessage("Account number is required")
                .MaximumLength(10).WithMessage("Account number must not exceed 10 characters")
                .MustAsync(CustomerExist).WithMessage("Customer does not exist");

            RuleFor(cmd => cmd.CustomerAddress.AddressTypeName)
                .NotEmpty().WithMessage("Address type is required")
                .MustAsync(AddressTypeExist).WithMessage("Address type does not exist");

            RuleFor(cmd => cmd.CustomerAddress.Address)
                .NotNull().WithMessage("Address is required");

            RuleFor(cmd => cmd.CustomerAddress.Address.AddressLine1)
                .NotEmpty().WithMessage("Address line 1 is required")
                .MaximumLength(60).WithMessage("Address line 1 must not exceed 60 characters");

            RuleFor(cmd => cmd.CustomerAddress.Address.AddressLine2)
                .MaximumLength(60).WithMessage("Address line 2 must not exceed 60 characters");

            RuleFor(cmd => cmd.CustomerAddress.Address.PostalCode)
                .NotEmpty().WithMessage("Postal code is required")
                .MaximumLength(15).WithMessage("Postal code must not exceed 15 characters");

            RuleFor(cmd => cmd.CustomerAddress.Address.City)
                .NotEmpty().WithMessage("City is required")
                .MaximumLength(30).WithMessage("City must not exceed 30 characters");

            RuleFor(cmd => cmd.CustomerAddress.Address.StateProvinceCode)
                .NotEmpty().WithMessage("State/province is required")
                .MaximumLength(3).WithMessage("State/province must not exceed 3 characters")
                .MustAsync(StateProvinceExist).WithMessage("State/province does not exist");

            RuleFor(cmd => cmd)
                .MustAsync(UniqueAddress).WithMessage("Address must be unique");
        }

        private async Task<bool> UniqueAddress(UpdateCustomerAddressCommand command, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.FirstOrDefaultAsync(new GetCustomerSpecification(command.AccountNumber));

            ICollection<BusinessEntityAddress> addresses = null;
            if (customer.Store != null)
                addresses = customer.Store.BusinessEntityAddresses;
            else if (customer.Person != null)
                addresses = customer.Person.BusinessEntityAddresses;

            var address = addresses.FirstOrDefault(a =>
                a.AddressType.Name == command.CustomerAddress.AddressTypeName &&
                a.Address.AddressLine1 == command.CustomerAddress.Address.AddressLine1 &&
                a.Address.AddressLine2 == command.CustomerAddress.Address.AddressLine2 &&
                a.Address.PostalCode == command.CustomerAddress.Address.PostalCode &&
                a.Address.City == command.CustomerAddress.Address.City &&
                a.Address.StateProvince.StateProvinceCode == command.CustomerAddress.Address.StateProvinceCode
            );

            return address == null;
        }

        private async Task<bool> CustomerExist(string accountNumber, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.FirstOrDefaultAsync(new GetCustomerSpecification(accountNumber));
            return customer != null;
        }

        private async Task<bool> AddressTypeExist(string name, CancellationToken cancellationToken)
        {
            var addressType = await addressTypeRepository.FirstOrDefaultAsync(new GetAddressTypeSpecification(name));
            return addressType != null;
        }

        private async Task<bool> StateProvinceExist(string stateProvinceCode, CancellationToken cancellationToken)
        {
            var stateProvince = await stateProvinceRepository.FirstOrDefaultAsync(new GetStateProvinceSpecification(stateProvinceCode));
            return stateProvince != null;
        }
    }
}