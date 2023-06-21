using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;

namespace AW.Services.Customer.Core.Handlers.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        private readonly IRepository<Entities.Customer> _customerRepository;

        public CreateCustomerCommandValidator(IRepository<Entities.Customer> customerRepository)
        {
            _customerRepository = customerRepository;

            RuleFor(cmd => cmd.Customer)
                .NotNull().WithMessage("Customer is required");

            RuleFor(cmd => cmd.Customer!.AccountNumber)
                .NotEmpty().WithMessage("Account number is required")
                .MaximumLength(10).WithMessage("Account number must not exceed 10 characters")
                .MustAsync(NotExists).WithMessage("Account number already exists")
                .When(cmd => cmd.Customer != null);
        }

        private async Task<bool> NotExists(string accountNumber, CancellationToken cancellationToken)
        {
            return !await _customerRepository.AnyAsync(
                new GetCustomerByAccountNumberSpecification(accountNumber),
                cancellationToken
            );
        }
    }
}
