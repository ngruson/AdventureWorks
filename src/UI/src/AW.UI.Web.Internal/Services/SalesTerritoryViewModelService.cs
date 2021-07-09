using AutoMapper;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi;
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
        private readonly IReferenceDataApiClient referenceDataApiClient;

        public SalesTerritoryViewModelService(
            ILogger<SalesTerritoryViewModelService> logger,
            IMapper mapper,
            IReferenceDataApiClient referenceDataApiClient
        ) =>
            (this.logger, this.mapper, this.referenceDataApiClient) =
                (logger, mapper, referenceDataApiClient);

        public async Task<SalesTerritoryIndexViewModel> GetSalesTerritories()
        {
            logger.LogInformation("GetSalesTerritories called");

            var territories = await referenceDataApiClient.GetTerritoriesAsync();

            var vm = new SalesTerritoryIndexViewModel
            {
                SalesTerritories = mapper.Map<IEnumerable<SalesTerritoryViewModel>>(territories)
            };

            return vm;
        }
    }
}