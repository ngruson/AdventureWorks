using MediatR;

namespace AW.Services.Customer.Core.Handlers.DeleteCustomerAddress
{
    public class DeleteCustomerAddressCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public CustomerAddressDto CustomerAddress { get; set; }
    }
}