using Ardalis.GuardClauses;
using Ardalis.Specification;
using AutoMapper;
using AW.Services.Product.Application.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Product.Application.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly ILogger<GetProductsQueryHandler> logger;
        private readonly IRepositoryBase<Domain.Product> repository;
        private readonly IMapper mapper;

        public GetProductsQueryHandler(
            ILogger<GetProductsQueryHandler> logger,
            IRepositoryBase<Domain.Product> repository, 
            IMapper mapper) 
            => (this.logger, this.repository, this.mapper) = (logger, repository, mapper);
        
        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");

            logger.LogInformation("Getting product from database");
            var spec = new GetProductsPaginatedSpecification(request.PageIndex, request.PageSize);
            var products = await repository.ListAsync(spec);

            Guard.Against.Null(products, nameof(products));

            logger.LogInformation("Returning products");
            return mapper.Map<IEnumerable<Product>>(products);
        }
    }
}