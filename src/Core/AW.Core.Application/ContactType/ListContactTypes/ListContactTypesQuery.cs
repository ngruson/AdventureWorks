using MediatR;
using System.Collections.Generic;

namespace AW.Core.Application.ContactType.ListContactTypes
{
    public class ListContactTypesQuery : IRequest<IEnumerable<string>>
    {
    }
}