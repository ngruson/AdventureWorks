using Ardalis.Result;
using MediatR;

namespace AW.Services.Customer.Core.Handlers.GetPreferredAddress
{
    public class GetPreferredAddressQuery : IRequest<Result<AddressDto?>>
    {
        public GetPreferredAddressQuery()
        {
        }
        public GetPreferredAddressQuery(Guid customerId, string addressType)
        {
            CustomerId = customerId;
            AddressType = addressType;
        }

        public Guid CustomerId { get; set; }
        public string? AddressType { get; set; }
    }
}
