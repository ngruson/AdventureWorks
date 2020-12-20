using Ardalis.Specification;
using AW.Core.Application.Specifications;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Core.Application.Customer.DeleteCustomerContact
{
    public class DeleteCustomerContactCommandValidator : AbstractValidator<DeleteCustomerContactCommand>
    {
        private readonly IRepositoryBase<Domain.Sales.Customer> customerRepository;
        private readonly IRepositoryBase<Domain.Person.ContactType> contactTypeRepository;

        public DeleteCustomerContactCommandValidator(IRepositoryBase<Domain.Sales.Customer> customerRepository,
            IRepositoryBase<Domain.Person.ContactType> contactTypeRepository)
        {
            this.customerRepository = customerRepository;
            this.contactTypeRepository = contactTypeRepository;

            RuleFor(cmd => cmd.AccountNumber)
                .NotEmpty().WithMessage("Account number is required")
                .MaximumLength(10).WithMessage("Account number must not exceed 10 characters")
                .MustAsync(CustomerExists).WithMessage("Customer does not exist");

            RuleFor(cmd => cmd.ContactTypeName)
                .NotEmpty().WithMessage("Contact type is required")
                .MustAsync(ContactTypeExists).WithMessage("Address type does not exist");

            RuleFor(cmd => cmd.Contact)
                .NotNull().WithMessage("Contact is required");

            RuleFor(cmd => cmd.Contact.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters")
                .When(cmd => cmd.Contact != null);

            RuleFor(cmd => cmd.Contact.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters")
                .When(cmd => cmd.Contact != null);
        }

        private async Task<bool> CustomerExists(string accountNumber, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetBySpecAsync(new GetCustomerSpecification(accountNumber));
            return customer != null;
        }

        private async Task<bool> ContactTypeExists(string name, CancellationToken cancellationToken)
        {
            var contactType = await contactTypeRepository.GetBySpecAsync(new GetContactTypeSpecification(name));
            return contactType != null;
        }
    }
}