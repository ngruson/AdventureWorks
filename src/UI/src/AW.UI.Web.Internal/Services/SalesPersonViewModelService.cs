using AutoMapper;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi;
using AW.UI.Web.Infrastructure.ApiClients.SalesPersonApi;
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
        private readonly ISalesPersonApiClient salesPersonApiClient;
        private readonly IReferenceDataApiClient referenceDataApiClient;

        public SalesPersonViewModelService(
            ILogger<SalesPersonViewModelService> logger,
            IMapper mapper,
            ISalesPersonApiClient salesPersonApiClient,
            IReferenceDataApiClient referenceDataApiClient
        ) =>
            (this.logger, this.mapper, this.salesPersonApiClient, this.referenceDataApiClient) = 
                (logger, mapper, salesPersonApiClient, referenceDataApiClient);

        public async Task<SalesPersonIndexViewModel> GetSalesPersons(string territory = null)
        {
            logger.LogInformation("GetSalesPersons called");
            var salesPersons = await salesPersonApiClient.GetSalesPersonsAsync(territory);

            var vm = new SalesPersonIndexViewModel
            {
                SalesPersons = mapper.Map<IEnumerable<SalesPersonViewModel>>(salesPersons),
                Territories = await GetTerritories(),
            };

            return vm;
        }

        private async Task<IEnumerable<SalesTerritoryViewModel>> GetTerritories()
        {
            var territories = await referenceDataApiClient.GetTerritoriesAsync();

            return mapper.Map<IEnumerable<SalesTerritoryViewModel>>(territories);
        }
    }
}