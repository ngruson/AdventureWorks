using AutoMapper;
using AW.Core.Abstractions.Api.SalesTerritoryApi;
using AW.UI.Web.Internal.Interfaces;
using AW.UI.Web.Internal.ViewModels.SalesTerritory;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.Services
{
    public class SalesTerritoryViewModelService : ISalesTerritoryViewModelService
    {
        private readonly ILogger<SalesTerritoryViewModelService> logger;
        private readonly IMapper mapper;
        private readonly ISalesTerritoryApi salesTerritoryApi;

        public SalesTerritoryViewModelService(
            ILogger<SalesTerritoryViewModelService> logger,
            IMapper mapper,
            ISalesTerritoryApi salesTerritoryApi
        ) =>
            (this.logger, this.mapper, this.salesTerritoryApi) =
                (logger, mapper, salesTerritoryApi);

        public async Task<SalesTerritoryIndexViewModel> GetSalesTerritories()
        {
            logger.LogInformation("GetSalesTerritories called");

            var response = await salesTerritoryApi.ListTerritoriesAsync();

            var vm = new SalesTerritoryIndexViewModel
            {
                SalesTerritories = mapper.Map<IEnumerable<SalesTerritoryViewModel>>(response.Territories)
            };

            return vm;
        }
    }
}