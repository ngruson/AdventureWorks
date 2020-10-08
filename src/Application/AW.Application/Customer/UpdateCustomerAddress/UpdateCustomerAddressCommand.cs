using MediatR;

namespace AW.Application.Customer.UpdateCustomerAddress
{
    public class UpdateCustomerAddressCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public CustomerAddressDto CustomerAddress { get; set; }
    }
}