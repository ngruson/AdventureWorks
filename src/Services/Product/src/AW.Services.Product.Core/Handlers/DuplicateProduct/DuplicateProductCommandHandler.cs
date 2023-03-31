using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Product.Core.Exceptions;
using AW.Services.Product.Core.GuardClauses;
using AW.Services.Product.Core.Handlers.CreateProduct;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Product.Core.Handlers.DuplicateProduct
{
    public class DuplicateProductCommandHandler : IRequestHandler<DuplicateProductCommand, Product>
    {
        private readonly ILogger<DuplicateProductCommandHandler> _logger;
        private readonly IRepository<Entities.Product> _productRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DuplicateProductCommandHandler(
            ILogger<DuplicateProductCommandHandler> logger,
            IRepository<Entities.Product> productRepository,
            IMediator mediator,
            IMapper mapper)
        {
            _logger = logger;
            _productRepository = productRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Product> Handle(DuplicateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting product to duplicate");
            var product = await _productRepository.SingleOrDefaultAsync(
                new GetProductSpecification(request.ProductNumber!),
                cancellationToken
            );
            Guard.Against.ProductNull(product, request.ProductNumber!, _logger);

            try
            {
                var command = new CreateProductCommand
                {
                    Product = _mapper.Map<CreateProduct.Product>(product)
                };

                int i = 0;
                while (true)
                {
                    string newProductNumber;
                    if (i == 0)
                        newProductNumber = $"Copy of {command.Product.ProductNumber}";
                    else
                        newProductNumber = $"Copy ({i}) of {command.Product.ProductNumber}";

                    product = await _productRepository.SingleOrDefaultAsync(
                        new GetProductSpecification(newProductNumber),
                        cancellationToken
                    );

                    if (product == null)
                    {
                        command.Product.ProductNumber = newProductNumber;
                        break;
                    }

                    i++;
                }
                
                command.Product.Name = $"Copy of {command.Product.Name}";

                _logger.LogInformation("Create duplicated product");
                var createdProduct = await _mediator.Send(command, cancellationToken);

                return _mapper.Map<Product>(createdProduct);
            }
            catch (Exception ex)
            {
                var duplicateException = new DuplicateProductException(request.ProductNumber, ex);
                _logger.LogError(duplicateException, "Duplicating product {ProductNumber} failed", request.ProductNumber);
                throw duplicateException;
            }
        }
    }
}
