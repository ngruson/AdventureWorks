using MediatR;
using System.Collections.Generic;

namespace AW.Services.ReferenceData.Core.Handlers.AddressType.GetAddressTypes
{
    public class GetAddressTypesQuery : IRequest<List<AddressType>>
    {
    }
}