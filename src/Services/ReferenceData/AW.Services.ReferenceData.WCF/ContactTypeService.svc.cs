using AW.Services.ReferenceData.Application.ContactType.GetContactTypes;
using AW.Services.ReferenceData.WCF.Messages.ListContactTypes;
using MediatR;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.WCF
{
    [ServiceBehavior(Namespace = "http://services.aw.com/ContactTypeService/1.0")]
    public class ContactTypeService : IContactTypeService
    {
        private readonly IMediator mediator;

        public ContactTypeService(IMediator mediator) => this.mediator = mediator;

        public async Task<ListContactTypesResponse> ListContactTypes()
        {
            var contactTypes = await mediator.Send(
                new GetContactTypesQuery()
            );

            return new ListContactTypesResponse
            {
                ContactTypes = contactTypes.Select(a => a.Name).ToList()
            };
        }
    }
}