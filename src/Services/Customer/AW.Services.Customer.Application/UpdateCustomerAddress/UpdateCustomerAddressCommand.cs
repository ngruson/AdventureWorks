using MediatR;

namespace AW.Services.Customer.Application.UpdateCustomerAddress
{
    public class UpdateCustomerAddressCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public CustomerAddressDto CustomerAddress { get; set; }
    }
}