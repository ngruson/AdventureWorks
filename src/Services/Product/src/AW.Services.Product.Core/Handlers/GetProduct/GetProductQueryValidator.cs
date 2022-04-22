using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

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
                .MustAsync(ProductExists).WithMessage("Product does not exist");
        }

        private async Task<bool> ProductExists(string productNumber, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetBySpecAsync(
                new GetProductSpecification(productNumber),
                cancellationToken
            );

            return product != null;
        }
    }
}