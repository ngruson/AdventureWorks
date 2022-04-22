using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Product.Core.Handlers.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product>
    {
        private readonly ILogger<GetProductQueryHandler> logger;
        private readonly IRepository<Entities.Product> repository;
        private readonly IMapper mapper;

        public GetProductQueryHandler(
            ILogger<GetProductQueryHandler> logger,
            IRepository<Entities.Product> repository,
            IMapper mapper)
            => (this.logger, this.repository, this.mapper) = (logger, repository, mapper);

        public async Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");

            var spec = new GetProductSpecification(request.ProductNumber);
            logger.LogInformation("Getting product from database");
            var product = await repository.GetBySpecAsync(spec, cancellationToken);

            Guard.Against.Null(product, nameof(product));

            logger.LogInformation("Returning product");
            return mapper.Map<Product>(product);
        }
    }
}