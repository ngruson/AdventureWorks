using MediatR;
using System.Collections.Generic;

namespace AW.Services.ReferenceData.Core.Handlers.ContactType.GetContactTypes
{
    public class GetContactTypesQuery : IRequest<List<ContactType>>
    {
    }
}