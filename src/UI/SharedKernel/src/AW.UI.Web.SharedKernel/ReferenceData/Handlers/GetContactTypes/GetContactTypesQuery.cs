using MediatR;

namespace AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetContactTypes
{
    public class GetContactTypesQuery : IRequest<List<ContactType>>
    {
    }
}