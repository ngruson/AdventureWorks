using Ardalis.Specification;
using AW.Application.Specifications;
using AW.Domain.Person;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.Customer.DeleteCustomerAddress
{
    public class DeleteCustomerAddressCommandValidator : AbstractValidator<DeleteCustomerAddressCommand>
    {
        private readonly IRepositoryBase<Domain.Sales.Customer> customerRepository;
        private readonly IRepositoryBase<Domain.Person.AddressType> addressTypeRepository;

        public DeleteCustomerAddressCommandValidator(IRepositoryBase<Domain.Sales.Customer> customerRepository,
            IRepositoryBase<Domain.Person.AddressType> addressTypeRepository)
        {
            this.customerRepository = customerRepository;
            this.addressTypeRepository = addressTypeRepository;

            RuleFor(cmd => cmd.AccountNumber)
                .NotEmpty().WithMessage("Account number is required")
                .MaximumLength(10).WithMessage("Account number must not exceed 10 characters")
                .MustAsync(CustomerExists).WithMessage("Customer does not exist");

            RuleFor(cmd => cmd.AddressTypeName)
                .NotEmpty().WithMessage("Address type is required")
                .MustAsync(AddressTypeExists).WithMessage("Address type does not exist");

            RuleFor(cmd => cmd)
                .MustAsync(AddressExistsWithAddressType).WithMessage("No address found with address type");
        }

        private async Task<bool> CustomerExists(string accountNumber, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetBySpecAsync(new GetCustomerSpecification(accountNumber));
            return customer != null;
        }

        private async Task<bool> AddressTypeExists(string name, CancellationToken cancellationToken)
        {
            var addressType = await addressTypeRepository.GetBySpecAsync(new GetAddressTypeSpecification(name));
            return addressType != null;
        }

        private async Task<bool> AddressExistsWithAddressType(DeleteCustomerAddressCommand command, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetBySpecAsync(new GetCustomerSpecification(command.AccountNumber));

            ICollection<BusinessEntityAddress> addresses = null;
            if (customer.Store != null)
                addresses = customer.Store.BusinessEntityAddresses;
            else if (customer.Person != null)
                addresses = customer.Person.BusinessEntityAddresses;

            var address = addresses.FirstOrDefault(a => a.AddressType.Name == command.AddressTypeName);

            return address != null;
        }
    }
}