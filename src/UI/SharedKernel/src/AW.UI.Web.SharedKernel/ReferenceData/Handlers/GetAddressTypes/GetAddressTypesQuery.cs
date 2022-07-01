using MediatR;

namespace AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetAddressTypes
{
    public class GetAddressTypesQuery : IRequest<List<AddressType>>
    {
    }
}