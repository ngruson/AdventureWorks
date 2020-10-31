using MediatR;
using System.Collections.Generic;

namespace AW.Application.ContactType.ListContactTypes
{
    public class ListContactTypesQuery : IRequest<IEnumerable<string>>
    {
    }
}