using MediatR;
using System.Collections.Generic;

namespace AW.Core.Application.AddressType.ListAddressTypes
{
    public class ListAddressTypesQuery : IRequest<IEnumerable<string>>
    {
    }
}