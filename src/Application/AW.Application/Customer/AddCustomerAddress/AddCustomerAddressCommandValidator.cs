using Ardalis.Specification;
using AW.Application.Specifications;
using AW.Domain.Person;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.Customer.AddCustomerAddress
{
    public class AddCustomerAddressCommandValidator : AbstractValidator<AddCustomerAddressCommand>
    {
        private readonly IRepositoryBase<Domain.Sales.Customer> customerRepository;
        private readonly IRepositoryBase<Domain.Person.AddressType> addressTypeRepository;
        private readonly IRepositoryBase<Domain.Person.StateProvince> stateProvinceRepository;

        public AddCustomerAddressCommandValidator(
            IRepositoryBase<Domain.Sales.Customer> customerRepository,
            IRepositoryBase<Domain.Person.AddressType> addressTypeRepository,
            IRepositoryBase<Domain.Person.StateProvince> stateProvinceRepository
        )
        {
            this.customerRepository = customerRepository;
            this.addressTypeRepository = addressTypeRepository;
            this.stateProvinceRepository = stateProvinceRepository;

            RuleFor(cmd => cmd.AccountNumber)
                .NotEmpty().WithMessage("Account number is required")
                .MaximumLength(10).WithMessage("Account number must not exceed 10 characters")
                .MustAsync(CustomerExist).WithMessage("Customer does not exist");

            RuleFor(cmd => cmd.CustomerAddress)
                .NotNull().WithMessage("Customer address is required");

            RuleFor(cmd => cmd.CustomerAddress.AddressTypeName)
                .NotEmpty().WithMessage("Address type is required")
                .MustAsync(AddressTypeExist).WithMessage("Address type does not exist")
                .When(cmd => cmd.CustomerAddress != null);

            RuleFor(cmd => cmd.CustomerAddress.Address)
                .NotNull().WithMessage("Address is required")
                .When(cmd => cmd.CustomerAddress != null);

            RuleFor(cmd => cmd.CustomerAddress.Address.AddressLine1)
                .NotEmpty().WithMessage("Address line 1 is required")
                .MaximumLength(60).WithMessage("Address line 1 must not exceed 60 characters")
                .When(cmd => cmd.CustomerAddress != null && cmd.CustomerAddress.Address != null);

            RuleFor(cmd => cmd.CustomerAddress.Address.AddressLine2)
                .MaximumLength(60).WithMessage("Address line 2 must not exceed 60 characters")
                .When(cmd => cmd.CustomerAddress != null && cmd.CustomerAddress.Address != null);

            RuleFor(cmd => cmd.CustomerAddress.Address.PostalCode)
                .NotEmpty().WithMessage("Postal code is required")
                .MaximumLength(15).WithMessage("Postal code must not exceed 15 characters")
                .When(cmd => cmd.CustomerAddress != null && cmd.CustomerAddress.Address != null);

            RuleFor(cmd => cmd.CustomerAddress.Address.City)
                .NotEmpty().WithMessage("City is required")
                .MaximumLength(30).WithMessage("City must not exceed 30 characters")
                .When(cmd => cmd.CustomerAddress != null && cmd.CustomerAddress.Address != null);

            RuleFor(cmd => cmd.CustomerAddress.Address.StateProvinceCode)
                .NotEmpty().WithMessage("State/province is required")
                .MaximumLength(3).WithMessage("State/province must not exceed 3 characters")
                .MustAsync(StateProvinceExist).WithMessage("State/province does not exist")
                .When(cmd => cmd.CustomerAddress != null && cmd.CustomerAddress.Address != null);

            RuleFor(cmd => cmd)
                .MustAsync(UniqueAddress).WithMessage("Address must be unique")
                .When(cmd => cmd.CustomerAddress != null && cmd.CustomerAddress.Address != null);
        }

        private async Task<bool> UniqueAddress(AddCustomerAddressCommand command, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetBySpecAsync(new GetCustomerSpecification(command.AccountNumber));

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
            var customer = await customerRepository.GetBySpecAsync(new GetCustomerSpecification(accountNumber));
            return customer != null;
        }

        private async Task<bool> AddressTypeExist(string name, CancellationToken cancellationToken)
        {
            var addressType = await addressTypeRepository.GetBySpecAsync(new GetAddressTypeSpecification(name));
            return addressType != null;
        }

        private async Task<bool> StateProvinceExist(string stateProvinceCode, CancellationToken cancellationToken)
        {
            var stateProvince = await stateProvinceRepository.GetBySpecAsync(new GetStateProvinceSpecification(stateProvinceCode));
            return stateProvince != null;
        }
    }
}