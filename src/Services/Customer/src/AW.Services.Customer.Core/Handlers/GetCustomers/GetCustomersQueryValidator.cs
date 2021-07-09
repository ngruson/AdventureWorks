using Ardalis.Specification;
using FluentValidation;

namespace AW.Services.Customer.Core.Handlers.GetCustomers
{
    public class GetCustomersQueryValidator : AbstractValidator<GetCustomersQuery>
    {
        public GetCustomersQueryValidator()
        {
            RuleFor(query => query.PageIndex)
                .GreaterThanOrEqualTo(0).WithMessage("Page index is required");

            RuleFor(query => query.PageSize)
                .GreaterThan(0).WithMessage("Page size is required");
        }
    }
}