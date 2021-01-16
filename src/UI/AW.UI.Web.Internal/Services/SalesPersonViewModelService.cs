using AutoMapper;
using AW.Core.Abstractions.Api.SalesPersonApi;
using AW.Core.Abstractions.Api.SalesPersonApi.ListSalesPersons;
using AW.Core.Abstractions.Api.SalesTerritoryApi;
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
        private readonly ISalesPersonApi salesPersonApi;
        private readonly ISalesTerritoryApi salesTerritoryApi;

        public SalesPersonViewModelService(
            ILogger<SalesPersonViewModelService> logger,
            IMapper mapper,
            ISalesPersonApi salesPersonApi,
            ISalesTerritoryApi salesTerritoryApi
        ) =>
            (this.logger, this.mapper, this.salesPersonApi, this.salesTerritoryApi) = 
                (logger, mapper, salesPersonApi, salesTerritoryApi);

        public async Task<SalesPersonIndexViewModel> GetSalesPersons()
        {
            logger.LogInformation("GetSalesPersons called");

            var response = await salesPersonApi.ListSalesPersonsAsync(
                new ListSalesPersonsRequest()
            );

            var vm = new SalesPersonIndexViewModel
            {
                SalesPersons = mapper.Map<IEnumerable<SalesPersonViewModel>>(response.SalesPersons),
                Territories = await GetTerritories(),
            };

            return vm;
        }

        private async Task<IEnumerable<SalesTerritoryViewModel>> GetTerritories()
        {
            var response = await salesTerritoryApi.ListTerritoriesAsync();

            return mapper.Map<IEnumerable<SalesTerritoryViewModel>>(response.Territories);
        }
    }
}