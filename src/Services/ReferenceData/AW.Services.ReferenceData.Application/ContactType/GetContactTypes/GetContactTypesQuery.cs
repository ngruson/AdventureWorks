using MediatR;
using System.Collections.Generic;

namespace AW.Services.ReferenceData.Application.ContactType.GetContactTypes
{
    public class GetContactTypesQuery : IRequest<List<ContactType>>
    {
    }
}