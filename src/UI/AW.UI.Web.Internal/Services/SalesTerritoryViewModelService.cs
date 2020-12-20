using AutoMapper;
using AW.Infrastructure.Api.WCF.SalesTerritoryService;
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
        private readonly ISalesTerritoryService salesTerritoryService;

        public SalesTerritoryViewModelService(
            ILogger<SalesTerritoryViewModelService> logger,
            IMapper mapper,
            ISalesTerritoryService salesTerritoryService
        ) =>
            (this.logger, this.mapper, this.salesTerritoryService) =
                (logger, mapper, salesTerritoryService);

        public async Task<SalesTerritoryIndexViewModel> GetSalesTerritories()
        {
            logger.LogInformation("GetSalesTerritories called");

            var response = await salesTerritoryService.ListTerritoriesAsync(
                new ListTerritoriesRequest()
            );

            var vm = new SalesTerritoryIndexViewModel
            {
                SalesTerritories = mapper.Map<IEnumerable<SalesTerritoryViewModel>>(response.ListTerritoriesResult)
            };

            return vm;
        }
    }
}