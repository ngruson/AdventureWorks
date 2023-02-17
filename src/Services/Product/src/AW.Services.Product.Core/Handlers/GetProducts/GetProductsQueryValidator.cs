using FluentValidation;

namespace AW.Services.Product.Core.Handlers.GetProducts
{
    public class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
    {
        public GetProductsQueryValidator()
        {
            RuleFor(cmd => cmd.PageIndex)
                .GreaterThan(-1)
                .WithMessage("Page index must be 0 or positive");

            RuleFor(cmd => cmd.PageSize)
                .GreaterThan(0)
                .WithMessage("Page size must be positive");

            RuleFor(cmd => cmd.OrderBy)
                .Must(BeValid)
                .WithMessage("Order by is not valid")
                .When(cmd => !string.IsNullOrEmpty(cmd.OrderBy));
        }

        private bool BeValid(string? orderBy)
        {
            if (!orderBy!.Contains('('))
                return false;

            if (!orderBy.Contains(')'))
                return false;

            string direction = orderBy[..orderBy.IndexOf('(')];
            if (!(direction == "asc" || direction == "desc"))
                return false;

            return true;
        }
    }
}
