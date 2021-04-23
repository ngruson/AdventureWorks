using Ardalis.Specification;
using AW.Services.Customer.Application.Specifications;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Application.AddCustomer
{
    public class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
    {
        private readonly IRepositoryBase<Domain.Customer> customerRepository;

        public AddCustomerCommandValidator(IRepositoryBase<Domain.Customer> customerRepository)
        {
            this.customerRepository = customerRepository;

            RuleFor(cmd => cmd.Customer)
                .NotNull().WithMessage("Customer is required");

            RuleFor(cmd => cmd.Customer.AccountNumber)
                .NotEmpty().WithMessage("Account number is required")
                .MaximumLength(10).WithMessage("Account number must not exceed 10 characters")
                .MustAsync(NotExists).WithMessage("Account number already exists")
                .When(cmd => cmd.Customer != null);
        }

        private async Task<bool> NotExists(string accountNumber, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetBySpecAsync(new GetCustomerSpecification(accountNumber));
            return customer == null;
        }
    }
}