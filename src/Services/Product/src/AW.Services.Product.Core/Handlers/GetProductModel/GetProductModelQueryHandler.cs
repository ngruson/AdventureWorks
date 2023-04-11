using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Product.Core.GuardClauses;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Product.Core.Handlers.GetProductModel
{
    public class GetProductModelQueryHandler : IRequestHandler<GetProductModelQuery, ProductModel?>
    {
        private readonly ILogger<GetProductModelQueryHandler> _logger;
        private readonly IRepository<Entities.ProductModel> _repository;
        private readonly IMapper _mapper;

        public GetProductModelQueryHandler(
            ILogger<GetProductModelQueryHandler> logger,
            IRepository<Entities.ProductModel> repository,
            IMapper mapper)
            => (_logger, _repository, _mapper) = (logger, repository, mapper);

        public async Task<ProductModel?> Handle(GetProductModelQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting product model from database");

            var spec = new GetProductModelSpecification(request.Name!);
            var productModel = await _repository.SingleOrDefaultAsync(spec, cancellationToken);
            Guard.Against.ProductModelNull(productModel, request.Name!, _logger);

            _logger.LogInformation("Returning product model");
            return _mapper.Map<ProductModel>(productModel);
        }
    }
}
