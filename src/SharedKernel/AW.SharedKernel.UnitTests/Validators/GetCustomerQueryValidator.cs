using AW.SharedKernel.UnitTests.Mediatr;
using FluentValidation;

namespace AW.SharedKernel.UnitTests.Validators
{
    public class GetCustomerQueryValidator : AbstractValidator<GetCustomerQuery>
    {
        public GetCustomerQueryValidator()
        {
            RuleFor(query => query.CustomerNumber)
                .NotEmpty()
                .WithMessage("Customer number is required");
        }
    }
}