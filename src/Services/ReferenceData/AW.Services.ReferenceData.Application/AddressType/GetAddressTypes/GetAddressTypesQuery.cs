using MediatR;
using System.Collections.Generic;

namespace AW.Services.ReferenceData.Application.AddressType.GetAddressTypes
{
    public class GetAddressTypesQuery : IRequest<List<AddressType>>
    {
    }
}