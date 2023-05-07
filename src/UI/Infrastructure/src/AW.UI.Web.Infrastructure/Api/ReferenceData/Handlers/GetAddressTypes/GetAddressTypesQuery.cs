using MediatR;

namespace AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetAddressTypes
{
    public class GetAddressTypesQuery : IRequest<List<AddressType>>
    {
    }
}