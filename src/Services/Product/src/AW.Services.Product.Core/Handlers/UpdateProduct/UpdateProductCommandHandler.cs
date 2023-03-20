﻿using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Product.Core.GuardClauses;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Product.Core.Handlers.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly ILogger<UpdateProductCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.Product> _productRepository;
        private readonly IRepository<Entities.ProductModel> _productModelRepository;

        public UpdateProductCommandHandler(
            ILogger<UpdateProductCommandHandler> logger,
            IMapper mapper,
            IRepository<Entities.Product> productRepository,
            IRepository<Entities.ProductModel> productModelRepository
        ) => 
            (_logger, _mapper, _productRepository, _productModelRepository) = 
                (logger, mapper, productRepository, productModelRepository);

        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");

            _logger.LogInformation("Getting product from database");
            var spec = new GetProductSpecification(request.Product!.ProductNumber!);
            var product = await _productRepository.SingleOrDefaultAsync(spec, cancellationToken);
            Guard.Against.ProductNull(product, request.Product.ProductNumber!, _logger);

            if (request.Product.ProductModelName != product!.ProductModel?.Name)
            {
                var productModel = await _productModelRepository.SingleOrDefaultAsync(
                    new GetProductModelSpecification(request.Product.ProductModelName!),
                    cancellationToken
                );

                Guard.Against.ProductModelNull(productModel, request.Product.ProductModelName!, _logger);
                if (product!.ProductModel != productModel)
                    product.SetProductModel(productModel!);
            }

            _logger.LogInformation("Updating product");
            _mapper.Map(request.Product, product);

            _logger.LogInformation("Saving product to database");
            await _productRepository.UpdateAsync(product!, cancellationToken);

            _logger.LogInformation("Returning product");
            return _mapper.Map<Product>(product);
        }
    }
}