using Ardalis.Specification;
using AW.Services.Product.Application.Specifications;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Product.Application.GetProduct
{
    public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
    {
        private readonly IRepositoryBase<Domain.Product> productRepository;

        public GetProductQueryValidator(IRepositoryBase<Domain.Product> productRepository)
        {
            this.productRepository = productRepository;

            RuleFor(cmd => cmd.ProductNumber)
                .NotEmpty().WithMessage("Product number is required")
                .MaximumLength(25).WithMessage("Product number must not exceed 25 characters")
                .MustAsync(ProductExists).WithMessage("Product does not exist");
        }

        private async Task<bool> ProductExists(string productNumber, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetBySpecAsync(new GetProductSpecification(productNumber));
            return product != null;
        }
    }
}