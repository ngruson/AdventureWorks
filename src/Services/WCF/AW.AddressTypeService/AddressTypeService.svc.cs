using AW.AddressTypeService.Messages.ListAddressTypes;
using AW.Core.Application.AddressType.ListAddressTypes;
using MediatR;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.AddressTypeService
{
    [ServiceBehavior(Namespace = "http://services.aw.com/AddressTypeService/1.0")]
    public class AddressTypeService : IAddressTypeService
    {
        private readonly IMediator mediator;

        public AddressTypeService(IMediator mediator) => this.mediator = mediator;

        public async Task<ListAddressTypesResponse> ListAddressTypes()
        {
            var addressTypes = await mediator.Send(
                new ListAddressTypesQuery()
            );

            return new ListAddressTypesResponse
            {
                AddressTypes = addressTypes.ToList()
            };
        }
    }
}