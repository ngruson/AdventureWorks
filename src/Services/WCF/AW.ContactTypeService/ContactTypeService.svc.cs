using AW.Application.ContactType.ListContactTypes;
using AW.ContactTypeService.Messages.ListContactTypes;
using MediatR;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.ContactTypeService
{
    [ServiceBehavior(Namespace = "http://services.aw.com/ContactTypeService/1.0")]
    public class ContactTypeService : IContactTypeService
    {
        private readonly IMediator mediator;

        public ContactTypeService(IMediator mediator) => this.mediator = mediator;

        public async Task<ListContactTypesResponse> ListContactTypes()
        {
            var contactTypes = await mediator.Send(
                new ListContactTypesQuery()
            );

            return new ListContactTypesResponse
            {
                ContactTypes = contactTypes.ToList()
            };
        }
    }
}