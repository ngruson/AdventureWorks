using AW.Services.ReferenceData.Application.StateProvince.GetStateProvinces;
using AW.Services.ReferenceData.WCF.Messages.ListStateProvinces;
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

        public async Task<ListStateProvincesResponse> ListStateProvinces(ListStateProvincesRequest request)
        {
            var stateProvinces = await mediator.Send(
                new GetStateProvincesQuery {  CountryRegionCode = request.CountryRegionCode }
            );

            return new ListStateProvincesResponse
            {
                StateProvinces = stateProvinces
            };
        }
    }
}