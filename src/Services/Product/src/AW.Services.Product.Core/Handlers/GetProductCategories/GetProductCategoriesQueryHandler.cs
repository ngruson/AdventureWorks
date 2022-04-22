using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Product.Core.Handlers.GetProductCategories
{
    public class GetProductCategoriesQueryHandler : IRequestHandler<GetProductCategoriesQuery, List<ProductCategory>>
    {
        private readonly ILogger<GetProductCategoriesQueryHandler> logger;
        private readonly IRepository<Entities.ProductCategory> repository;
        private readonly IMapper mapper;

        public GetProductCategoriesQueryHandler(
            ILogger<GetProductCategoriesQueryHandler> logger,
            IRepository<Entities.ProductCategory> repository,
            IMapper mapper)
            => (this.logger, this.repository, this.mapper) = (logger, repository, mapper);

        public async Task<List<ProductCategory>> Handle(GetProductCategoriesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");

            logger.LogInformation("Getting product categories from database");
            var categories = await repository.ListAsync(
                new GetProductCategoriesSpecification(),
                cancellationToken
            );

            Guard.Against.Null(categories, nameof(categories));

            logger.LogInformation("Returning product categories");
            return mapper.Map<List<ProductCategory>>(categories);
        }
    }
}