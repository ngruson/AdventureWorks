using MediatR;

namespace AW.Services.Customer.Core.Handlers.GetPreferredAddress
{
    public class GetPreferredAddressQuery : IRequest<AddressDto>
    {
        public string AccountNumber { get; set; }
        public string AddressType { get; set; }
    }
}