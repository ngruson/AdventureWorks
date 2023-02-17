using MediatR;

namespace AW.Services.Customer.Core.Handlers.GetPreferredAddress
{
    public class GetPreferredAddressQuery : IRequest<AddressDto?>
    {
        public GetPreferredAddressQuery()
        {
        }
        public GetPreferredAddressQuery(string accountNumber, string addressType)
        {
            AccountNumber = accountNumber;
            AddressType = addressType;
        }

        public string? AccountNumber { get; set; }
        public string? AddressType { get; set; }
    }
}
