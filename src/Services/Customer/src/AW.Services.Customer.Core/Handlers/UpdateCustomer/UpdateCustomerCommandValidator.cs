using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;

namespace AW.Services.Customer.Core.Handlers.UpdateCustomer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        private readonly IRepository<Entities.Customer> customerRepository;

        public UpdateCustomerCommandValidator(IRepository<Entities.Customer> customerRepository)
        {
            this.customerRepository = customerRepository;

            RuleFor(cmd => cmd.Customer)
                .NotNull().WithMessage("Customer is required");

            RuleFor(cmd => cmd.Customer!.AccountNumber)
                .NotEmpty().WithMessage("Account number is required")
                .MaximumLength(10).WithMessage("Account number must not exceed 10 characters")
                .MustAsync(CustomerExists).WithMessage("Customer does not exist")
                .When(cmd => cmd.Customer != null);
        }

        private async Task<bool> CustomerExists(string accountNumber, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.SingleOrDefaultAsync(
                new GetCustomerSpecification(accountNumber),
                cancellationToken
            );

            return customer != null;
        }
    }
}