using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Product.Core.GuardClauses;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Product.Core.Handlers.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product>
    {
        private readonly ILogger<GetProductQueryHandler> _logger;
        private readonly IRepository<Entities.Product> _repository;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(
            ILogger<GetProductQueryHandler> logger,
            IRepository<Entities.Product> repository,
            IMapper mapper)
            => (_logger, _repository, _mapper) = (logger, repository, mapper);

        public async Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");

            var spec = new GetProductSpecification(request.ProductNumber);
            _logger.LogInformation("Getting product from database");
            var product = await _repository.SingleOrDefaultAsync(spec, cancellationToken);

            Guard.Against.ProductNull(product, request.ProductNumber!, _logger);

            _logger.LogInformation("Returning product");
            return _mapper.Map<Product>(product);
        }
    }
}
