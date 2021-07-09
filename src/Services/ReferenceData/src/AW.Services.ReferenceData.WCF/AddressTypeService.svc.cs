using AW.Services.ReferenceData.Core.Handlers.AddressType.GetAddressTypes;
using AW.Services.ReferenceData.WCF.Messages.ListAddressTypes;
using MediatR;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.WCF
{
    [ServiceBehavior(Namespace = "http://services.aw.com/AddressTypeService/1.0")]
    public class AddressTypeService : IAddressTypeService
    {
        private readonly IMediator mediator;

        public AddressTypeService(IMediator mediator) => this.mediator = mediator;

        public async Task<ListAddressTypesResponse> ListAddressTypes()
        {
            var addressTypes = await mediator.Send(
                new GetAddressTypesQuery()
            );

            return new ListAddressTypesResponse
            {
                AddressTypes = addressTypes.Select(a => a.Name).ToList()
            };
        }
    }
}