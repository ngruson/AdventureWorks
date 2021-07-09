using MediatR;

namespace AW.Services.Customer.Core.Handlers.AddCustomerAddress
{
    public class AddCustomerAddressCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public CustomerAddressDto CustomerAddress { get; set; }
    }
}