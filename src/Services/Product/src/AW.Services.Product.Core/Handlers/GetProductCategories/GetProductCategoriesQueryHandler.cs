using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Product.Core.Handlers.GetProductCategories
{
    public class GetProductCategoriesQueryHandler : IRequestHandler<GetProductCategoriesQuery, List<ProductCategory>>
    {
        private readonly ILogger<GetProductCategoriesQueryHandler> _logger;
        private readonly IRepository<Entities.ProductCategory> _repository;
        private readonly IMapper _mapper;

        public GetProductCategoriesQueryHandler(
            ILogger<GetProductCategoriesQueryHandler> logger,
            IRepository<Entities.ProductCategory> repository,
            IMapper mapper)
            => (_logger, _repository, _mapper) = (logger, repository, mapper);

        public async Task<List<ProductCategory>> Handle(GetProductCategoriesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");

            _logger.LogInformation("Getting product categories from database");
            var categories = await _repository.ListAsync(
                new GetProductCategoriesSpecification(),
                cancellationToken
            );

            Guard.Against.Null(categories, _logger);

            _logger.LogInformation("Returning product categories");
            return _mapper.Map<List<ProductCategory>>(categories);
        }
    }
}