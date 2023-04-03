using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;

namespace AW.Services.Product.Core.Handlers.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IRepository<Entities.Product> _productRepository;

        public CreateProductCommandValidator(IRepository<Entities.Product> productRepository)
        {
            _productRepository = productRepository;

            RuleFor(cmd => cmd.Product!.Name)
                .NotEmpty().WithMessage("Name is required");

            RuleFor(cmd => cmd.Product!.ProductNumber)
                .NotEmpty().WithMessage("Product number is required")
                .MustAsync(NotExists).WithMessage("Product number already exists")
                .When(cmd => cmd.Product != null);

            RuleFor(cmd => cmd.Product!.SellStartDate)
                .NotEmpty().WithMessage("Sell start date is required");
        }

        private async Task<bool> NotExists(string? productNumber, CancellationToken cancellationToken)
        {
            var exists = await _productRepository.AnyAsync(
                new ProductExistsSpecification(productNumber!),
                cancellationToken
            );

            return !exists;
        }
    }
}
