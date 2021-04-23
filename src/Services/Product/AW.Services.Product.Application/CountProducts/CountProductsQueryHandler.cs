using Ardalis.GuardClauses;
using Ardalis.Specification;
using AW.Services.Product.Application.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Product.Application.CountProducts
{
    public class CountProductsQueryHandler : IRequestHandler<CountProductsQuery, int>
    {
        private readonly ILogger<CountProductsQueryHandler> logger;
        private readonly IRepositoryBase<Domain.Product> repository;

        public CountProductsQueryHandler(
            ILogger<CountProductsQueryHandler> logger,
            IRepositoryBase<Domain.Product> repository)
            => (this.logger, this.repository) = (logger, repository);

        public async Task<int> Handle(CountProductsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");

            logger.LogInformation("Getting product count from database");
            var spec = new GetProductsCountSpecification();
            var count = await repository.CountAsync(spec);

            Guard.Against.Null(count, nameof(count));

            logger.LogInformation("Returning product count");
            return count;
        }
    }
}