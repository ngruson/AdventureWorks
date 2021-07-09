using AW.Services.ReferenceData.Core.Handlers.StateProvince.GetStatesProvinces;
using AW.Services.ReferenceData.WCF.Messages.ListStatesProvinces;
using MediatR;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.WCF
{
    [ServiceBehavior(Namespace = "http://services.aw.com/CountryRegionService/1.0")]
    public class StateProvinceService : IStateProvinceService
    {
        private readonly IMediator mediator;

        public StateProvinceService(IMediator mediator) => this.mediator = mediator;

        public async Task<ListStatesProvincesResponse> ListStatesProvinces(ListStatesProvincesRequest request)
        {
            var stateProvinces = await mediator.Send(
                new GetStatesProvincesQuery {  CountryRegionCode = request.CountryRegionCode }
            );

            return new ListStatesProvincesResponse
            {
                StateProvinces = stateProvinces
            };
        }
    }
}