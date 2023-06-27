using Ardalis.Result;
using MediatR;

namespace AW.Services.ReferenceData.Core.Handlers.AddressType.GetAddressTypes;

public class GetAddressTypesQuery : IRequest<Result<List<AddressType>>>
{
}
