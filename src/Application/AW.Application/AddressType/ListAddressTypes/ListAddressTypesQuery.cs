using MediatR;
using System.Collections.Generic;

namespace AW.Application.AddressType.ListAddressTypes
{
    public class ListAddressTypesQuery : IRequest<IEnumerable<string>>
    {
    }
}