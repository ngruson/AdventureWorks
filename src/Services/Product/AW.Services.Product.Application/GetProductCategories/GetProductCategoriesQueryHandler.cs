using Ardalis.GuardClauses;
using Ardalis.Specification;
using AutoMapper;
using AW.Services.Product.Application.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Product.Application.GetProductCategories
{
    public class GetProductCategoriesQueryHandler : IRequestHandler<GetProductCategoriesQuery, List<ProductCategory>>
    {
        private readonly IMediator mediator;
        private readonly ILogger<GetProductCategoriesQueryHandler> logger;
        private readonly IRepositoryBase<Domain.ProductCategory> repository;
        private readonly IMapper mapper;

        public GetProductCategoriesQueryHandler(
            IMediator mediator,
            ILogger<GetProductCategoriesQueryHandler> logger,
            IRepositoryBase<Domain.ProductCategory> repository,
            IMapper mapper)
            => (this.mediator, this.logger, this.repository, this.mapper) = (mediator, logger, repository, mapper);

        public async Task<List<ProductCategory>> Handle(GetProductCategoriesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");

            logger.LogInformation("Getting product categories from database");
            var categories = await repository.ListAsync(
                new GetProductCategoriesSpecification()
            );

            Guard.Against.Null(categories, nameof(categories));

            logger.LogInformation("Returning product categories");
            return mapper.Map<List<ProductCategory>>(categories);
        }
    }
}