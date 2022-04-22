using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Product.Core.Handlers.CountProducts
{
    public class CountProductsQueryHandler : IRequestHandler<CountProductsQuery, int>
    {
        private readonly ILogger<CountProductsQueryHandler> logger;
        private readonly IRepository<Entities.Product> repository;

        public CountProductsQueryHandler(
            ILogger<CountProductsQueryHandler> logger,
            IRepository<Entities.Product> repository)
            => (this.logger, this.repository) = (logger, repository);

        public async Task<int> Handle(CountProductsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");

            logger.LogInformation("Getting product count from database");
            var spec = new GetProductsCountSpecification();
            var count = await repository.CountAsync(spec, cancellationToken);

            logger.LogInformation("Returning product count");
            return count;
        }
    }
}