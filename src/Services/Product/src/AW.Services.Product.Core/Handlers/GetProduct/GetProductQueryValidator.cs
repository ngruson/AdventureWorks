using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;

namespace AW.Services.Product.Core.Handlers.GetProduct
{
    public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
    {
        private readonly IRepository<Entities.Product> productRepository;

        public GetProductQueryValidator(IRepository<Entities.Product> productRepository)
        {
            this.productRepository = productRepository;

            RuleFor(cmd => cmd.ProductNumber)
                .NotEmpty().WithMessage("Product number is required")
                .MaximumLength(25).WithMessage("Product number must not exceed 25 characters")
                .Must(ProductExists).WithMessage("Product does not exist");
        }

        private bool ProductExists(string productNumber)
        {
            var product = productRepository.SingleOrDefaultAsync(
                new GetProductSpecification(productNumber),
                CancellationToken.None
            ).GetAwaiter().GetResult();

            return product != null;
        }
    }
}
