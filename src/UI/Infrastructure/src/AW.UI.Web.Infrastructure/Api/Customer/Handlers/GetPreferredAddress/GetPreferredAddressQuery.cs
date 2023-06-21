using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetPreferredAddress;

public class GetPreferredAddressQuery : IRequest<Address>
{
    public GetPreferredAddressQuery(string? accountNumber, string? addressType)
    {
        AccountNumber = accountNumber;
        AddressType = addressType;
    }

    public string? AccountNumber { get; set; }
    public string? AddressType { get; set; }
}
