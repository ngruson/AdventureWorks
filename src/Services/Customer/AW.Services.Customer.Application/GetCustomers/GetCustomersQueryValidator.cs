using Ardalis.Specification;
using FluentValidation;

namespace AW.Services.Customer.Application.GetCustomers
{
    public class GetCustomersQueryValidator : AbstractValidator<GetCustomersQuery>
    {
        public GetCustomersQueryValidator()
        {
            RuleFor(cmd => cmd.PageIndex)
                .NotEmpty().WithMessage("Page index is required");

            RuleFor(cmd => cmd.PageSize)
                .NotEmpty().WithMessage("Page size is required");
        }
    }
}