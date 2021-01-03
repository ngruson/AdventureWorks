using AutoMapper;
using AW.Core.Abstractions.Api.SalesTerritoryApi;
using AW.Core.Abstractions.Api.SalesTerritoryApi.ListTerritories;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AW.Infrastructure.Api.WCF
{
    public class SalesTerritoryServiceAdapter : ISalesTerritoryApi
    {
        private readonly ILogger<SalesTerritoryServiceAdapter> logger;
        private readonly IMapper mapper;
        private readonly SalesTerritoryService.ISalesTerritoryService salesTerritoryService;

        public SalesTerritoryServiceAdapter(
            ILogger<SalesTerritoryServiceAdapter> logger,
            IMapper mapper,
            SalesTerritoryService.ISalesTerritoryService salesTerritoryService
        ) => (this.logger, this.mapper, this.salesTerritoryService) = (logger, mapper, salesTerritoryService);

        public async Task<ListTerritoriesResponse> ListTerritoriesAsync()
        {
            logger.LogInformation("Calling ListTerritories operation of Country web service");
            var response = await salesTerritoryService.ListTerritoriesAsync(
                new SalesTerritoryService.ListTerritoriesRequest() 
            );
            logger.LogInformation("ListTerritories operation executed succesfully");

            return mapper.Map<ListTerritoriesResponse>(response);
        }
    }
}