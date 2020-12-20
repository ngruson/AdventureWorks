using AutoMapper;
using AW.Infrastructure.Api.WCF.SalesPersonService;
using AW.Infrastructure.Api.WCF.SalesTerritoryService;
using AW.UI.Web.Internal.Interfaces;
using AW.UI.Web.Internal.ViewModels.SalesPerson;
using AW.UI.Web.Internal.ViewModels.SalesTerritory;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.Services
{
    public class SalesPersonViewModelService : ISalesPersonViewModelService
    {
        private readonly ILogger<SalesPersonViewModelService> logger;
        private readonly IMapper mapper;
        private readonly ISalesPersonService salesPersonService;
        private readonly ISalesTerritoryService salesTerritoryService;

        public SalesPersonViewModelService(
            ILogger<SalesPersonViewModelService> logger,
            IMapper mapper,
            ISalesPersonService salesPersonService,
            ISalesTerritoryService salesTerritoryService
        ) =>
            (this.logger, this.mapper, this.salesPersonService, this.salesTerritoryService) = 
                (logger, mapper, salesPersonService, salesTerritoryService);

        public async Task<SalesPersonIndexViewModel> GetSalesPersons()
        {
            logger.LogInformation("GetSalesPersons called");

            var response = await salesPersonService.ListSalesPersonsAsync(
                new ListSalesPersonsRequest1
                {
                    request = new ListSalesPersonsRequest()
                }
            );

            var vm = new SalesPersonIndexViewModel
            {
                SalesPersons = mapper.Map<IEnumerable<SalesPersonViewModel>>(response.ListSalesPersonsResult),
                Territories = await GetTerritories(),
            };

            return vm;
        }

        private async Task<IEnumerable<SalesTerritoryViewModel>> GetTerritories()
        {
            var response = await salesTerritoryService.ListTerritoriesAsync(
                new ListTerritoriesRequest()
            );

            return mapper.Map<IEnumerable<SalesTerritoryViewModel>>(response.ListTerritoriesResult);
        }
    }
}