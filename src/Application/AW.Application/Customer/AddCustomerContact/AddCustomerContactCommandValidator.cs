using Ardalis.Specification;
using AW.Application.Specifications;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.Customer.AddCustomerContact
{
    public class AddCustomerContactCommandValidator : AbstractValidator<AddCustomerContactCommand>
    {
        private readonly IRepositoryBase<Domain.Sales.Customer> customerRepository;
        private readonly IRepositoryBase<Domain.Person.ContactType> contactTypeRepository;

        public AddCustomerContactCommandValidator(
            IRepositoryBase<Domain.Sales.Customer> customerRepository,
            IRepositoryBase<Domain.Person.ContactType> contactTypeRepository
        )
        {
            this.customerRepository = customerRepository;
            this.contactTypeRepository = contactTypeRepository;

            RuleFor(cmd => cmd.AccountNumber)
                .NotEmpty().WithMessage("Account number is required")
                .MaximumLength(10).WithMessage("Account number must not exceed 10 characters")
                .MustAsync(CustomerExist).WithMessage("Customer does not exist");

            RuleFor(cmd => cmd.CustomerContact)
                .NotNull().WithMessage("Customer contact is required");

            RuleFor(cmd => cmd.CustomerContact.ContactTypeName)
                .NotEmpty().WithMessage("Contact type is required")
                .MustAsync(ContactTypeExist).WithMessage("Contact type does not exist")
                .When(cmd => cmd.CustomerContact != null);

            RuleFor(cmd => cmd.CustomerContact.Contact)
                .NotNull().WithMessage("Contact is required")
                .When(cmd => cmd.CustomerContact != null);

            RuleFor(cmd => cmd.CustomerContact.Contact.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters")
                .When(cmd => cmd.CustomerContact != null && cmd.CustomerContact.Contact != null);

            RuleFor(cmd => cmd.CustomerContact.Contact.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters")
                .When(cmd => cmd.CustomerContact != null && cmd.CustomerContact.Contact != null);
        }

        private async Task<bool> CustomerExist(string accountNumber, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetBySpecAsync(new GetCustomerSpecification(accountNumber));
            return customer != null;
        }

        private async Task<bool> ContactTypeExist(string name, CancellationToken cancellationToken)
        {
            var contactType = await contactTypeRepository.GetBySpecAsync(new GetContactTypeSpecification(name));
            return contactType != null;
        }
    }
}