using AW.Application.Interfaces;
using AW.Application.Specifications;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.Customer.GetCustomer
{
    public class GetCustomerQueryValidator : AbstractValidator<GetCustomerQuery>
    {
        private readonly IAsyncRepository<Domain.Sales.Customer> customerRepository;

        public GetCustomerQueryValidator(IAsyncRepository<Domain.Sales.Customer> customerRepository)
        {
            this.customerRepository = customerRepository;

            RuleFor(cmd => cmd.AccountNumber)
                .NotEmpty().WithMessage("Account number is required")
                .MaximumLength(10).WithMessage("Account number must not exceed 10 characters")
                .MustAsync(CustomerExists).WithMessage("Customer does not exist");

        }

        private async Task<bool> CustomerExists(string accountNumber, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.FirstOrDefaultAsync(new GetCustomerSpecification(accountNumber));
            return customer != null;
        }
    }
}