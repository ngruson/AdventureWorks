using AW.SharedKernel.Caching;
using AW.UI.Web.Internal.Interfaces;
using AW.UI.Web.Internal.ViewModels.SalesTerritory;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetTerritories;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.Services
{
    public class SalesTerritoryViewModelService : ISalesTerritoryViewModelService
    {
        private readonly ILogger<SalesTerritoryViewModelService> logger;
        private readonly ICache<Territory> cache;

        public SalesTerritoryViewModelService(
            ILogger<SalesTerritoryViewModelService> logger,
            ICache<Territory> cache
        ) =>
            (this.logger, this.cache) = (logger, cache);

        public async Task<SalesTerritoryIndexViewModel> GetSalesTerritories()
        {
            logger.LogInformation("GetSalesTerritories called");

            var territories = await cache.GetData();

            var vm = new SalesTerritoryIndexViewModel
            {
                SalesTerritories = territories
            };

            return vm;
        }
    }
}