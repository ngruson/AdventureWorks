using AutoMapper;
using AW.Core.Abstractions.Api.StateProvinceApi;
using AW.Core.Abstractions.Api.StateProvinceApi.ListStateProvinces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AW.Infrastructure.Api.WCF
{
    public class StateProvinceServiceWCF : IStateProvinceApi
    {
        private readonly ILogger<StateProvinceServiceWCF> logger;
        private readonly IMapper mapper;
        private readonly StateProvinceService.IStateProvinceService stateProvinceService;

        public StateProvinceServiceWCF(
            ILogger<StateProvinceServiceWCF> logger,
            IMapper mapper,
            StateProvinceService.IStateProvinceService stateProvinceService
        ) => (this.logger, this.mapper, this.stateProvinceService) = (logger, mapper, stateProvinceService);

        public async Task<ListStateProvincesResponse> ListStateProvincesAsync(ListStateProvincesRequest request)
        {
            logger.LogInformation("Mapping to ListStateProvincesRequest");
            var req = mapper.Map<StateProvinceService.ListStateProvincesRequest>(request);

            logger.LogInformation("Calling ListStateProvinces operation of StateProvince web service");
            var response = await stateProvinceService.ListStateProvincesAsync(req);
            logger.LogInformation("ListStateProvinces operation executed succesfully");

            return mapper.Map<ListStateProvincesResponse>(response);
        }
    }
}