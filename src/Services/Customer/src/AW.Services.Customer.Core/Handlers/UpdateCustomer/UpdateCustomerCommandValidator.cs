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
                .When(cmd => cmd.Customer != null);

            RuleFor(_ => _.Customer!.ObjectId)
                .MustAsync(CustomerExists).WithMessage("Customer does not exist")
                .When(cmd => cmd.Customer != null);
        }

        private async Task<bool> CustomerExists(Guid objectId, CancellationToken cancellationToken)
        {
            return await customerRepository.AnyAsync(
                new GetCustomerSpecification(objectId),
                cancellationToken
            );
        }
    }
}
