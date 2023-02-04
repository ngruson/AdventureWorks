using AW.SharedKernel.Caching;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Product.Handlers.GetProductCategories
{
    public class GetProductCategoriesQueryHandler : IRequestHandler<GetProductCategoriesQuery, List<ProductCategory>?>
    {
        private readonly ILogger<GetProductCategoriesQueryHandler> logger;
        private readonly ICache<ProductCategory> cache;

        public GetProductCategoriesQueryHandler(ILogger<GetProductCategoriesQueryHandler> logger, ICache<ProductCategory> cache)
        {
            this.logger = logger;
            this.cache = cache;
        }

        public async Task<List<ProductCategory>?> Handle(GetProductCategoriesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all product categories from cache");
            return await cache.GetData();
        }
    }
}