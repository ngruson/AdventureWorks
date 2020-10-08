using AutoMapper;
using AW.Application.StateProvince.ListStateProvinces;
using AW.StateProvinceService.Messages.ListStateProvinces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AW.StateProvinceService
{
    public class StateProvinceService : IStateProvinceService
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public StateProvinceService(IMediator mediator, IMapper mapper) => (this.mediator, this.mapper) = (mediator, mapper);
        
        public async Task<ListStateProvincesResponse> ListStateProvinces(ListStateProvincesRequest request)
        {
            var stateProvinces = await mediator.Send(
                new ListStateProvincesQuery
                {
                    CountryRegionCode = request.CountryRegionCode
                }
            );

            return new ListStateProvincesResponse
            {
                StateProvinces = mapper.Map<List<StateProvince>>(stateProvinces.ToList())
            };
        }
    }
}