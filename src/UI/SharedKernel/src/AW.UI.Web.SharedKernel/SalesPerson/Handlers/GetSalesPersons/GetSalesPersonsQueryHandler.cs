using AW.SharedKernel.Caching;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.SalesPerson.Handlers.GetSalesPersons
{
    public class GetSalesPersonsQueryHandler : IRequestHandler<GetSalesPersonsQuery, List<SalesPerson>>
    {
        private readonly ILogger<GetSalesPersonsQueryHandler> logger;
        private readonly ICache<SalesPerson> cache;

        public GetSalesPersonsQueryHandler(ILogger<GetSalesPersonsQueryHandler> logger, ICache<SalesPerson> cache) => (this.logger, this.cache) = (logger, cache);

        public async Task<List<SalesPerson>> Handle(GetSalesPersonsQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Territory))
            {
                logger.LogInformation("Getting all sales persons from cache");
                return await cache.GetData();
            }
            else
            {
                logger.LogInformation("Getting sales persons from cache for {Territory}", request.Territory);
                return await cache.GetData(sp => sp.Territory == request.Territory);
            }
        }
    }
}