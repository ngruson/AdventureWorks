using MediatR;

namespace AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetContactTypes
{
    public class GetContactTypesQuery : IRequest<List<ContactType>>
    {
    }
}