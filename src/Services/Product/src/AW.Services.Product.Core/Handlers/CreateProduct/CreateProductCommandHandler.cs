using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Product.Core.Entities;
using AW.Services.Product.Core.GuardClauses;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Product.Core.Handlers.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly ILogger<CreateProductCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.Product> _productRepository;
        private readonly IRepository<ProductSubcategory> _productSubcategoryRepository;
        private readonly IRepository<ProductModel> _productModelRepository;
        private readonly IRepository<UnitMeasure> _unitMeasureRepository;

        public CreateProductCommandHandler(
            ILogger<CreateProductCommandHandler> logger,
            IMapper mapper,
            IRepository<Entities.Product> productRepository,
            IRepository<ProductSubcategory> productSubcategoryRepository,
            IRepository<ProductModel> productModelRepository,
            IRepository<UnitMeasure> unitMeasureRepository
        )
        {
            _logger = logger;
            _mapper = mapper;
            _productRepository = productRepository;
            _productSubcategoryRepository = productSubcategoryRepository;
            _productModelRepository = productModelRepository;
            _unitMeasureRepository = unitMeasureRepository;
        }

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Map to Product entity");
            var newProduct = _mapper.Map<Entities.Product>(request.Product);
            
            if (!string.IsNullOrEmpty(request.Product!.ProductSubcategoryName!))
                newProduct.SetSubcategory(                
                    await GetSubcategory(
                        request.Product!.ProductSubcategoryName!,
                        cancellationToken
                    )
                );

            if (!string.IsNullOrEmpty(request.Product!.ProductModelName!))
                newProduct.SetProductModel(
                    await GetProductModel(
                        request.Product.ProductModelName!,
                        cancellationToken
                    )
                );

            if (!string.IsNullOrEmpty(request.Product.SizeUnitMeasureCode))
                newProduct.SetSizeUnitMeasure(
                    await GetUnitMeasure(
                        request.Product.SizeUnitMeasureCode!,
                        cancellationToken
                    )
                );

            if (!string.IsNullOrEmpty(request.Product.WeightUnitMeasureCode))
                newProduct.SetWeightUnitMeasure(
                await GetUnitMeasure(
                    request.Product.WeightUnitMeasureCode!,
                    cancellationToken
                )
            );

            _logger.LogInformation("Save new product to database");
            await _productRepository.AddAsync(newProduct, cancellationToken);

            _logger.LogInformation("Return new product");
            return _mapper.Map<Product>(newProduct);
        }

        private async Task<ProductSubcategory> GetSubcategory(string name, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(name, _logger);
            var subcategory = await _productSubcategoryRepository.SingleOrDefaultAsync(
                new GetProductSubcategorySpecification(name),
                cancellationToken
            );
            Guard.Against.ProductSubcategoryNull(subcategory, name, _logger);

            return subcategory!;
        }

        private async Task<ProductModel> GetProductModel(string name, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(name, _logger);
            var productModel = await _productModelRepository.SingleOrDefaultAsync(
                new GetProductModelSpecification(name),
                cancellationToken
            );
            Guard.Against.ProductModelNull(productModel, name, _logger);

            return productModel!;
        }

        private async Task<UnitMeasure> GetUnitMeasure(string code, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(code, _logger);
            var unitMeasure = await _unitMeasureRepository.SingleOrDefaultAsync(
                new GetUnitMeasureSpecification(code),
                cancellationToken
            );
            Guard.Against.UnitMeasureNull(unitMeasure, code, _logger);

            return unitMeasure!;
        }
    }
}
