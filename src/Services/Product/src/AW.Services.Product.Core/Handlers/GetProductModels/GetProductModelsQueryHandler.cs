using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Product.Core.GuardClauses;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Product.Core.Handlers.GetProductModels
{
    public class GetProductModelsQueryHandler : IRequestHandler<GetProductModelsQuery, List<ProductModel>>
    {
        private readonly ILogger<GetProductModelsQueryHandler> _logger;
        private readonly IRepository<Entities.ProductModel> _repository;
        private readonly IMapper _mapper;

        public GetProductModelsQueryHandler(
            ILogger<GetProductModelsQueryHandler> logger,
            IRepository<Entities.ProductModel> repository,
            IMapper mapper)
            => (_logger, _repository, _mapper) = (logger, repository, mapper);

        public async Task<List<ProductModel>> Handle(GetProductModelsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");
            _logger.LogInformation("Getting product models from database");

            var spec = new GetProductModelsSpecification();
            var productModels = await _repository.ListAsync(spec, cancellationToken);
            Guard.Against.ProductModelsNullOrEmpty(productModels, _logger);

            _logger.LogInformation("Returning product models");
            return _mapper.Map<List<ProductModel>>(productModels);
        }
    }
}
