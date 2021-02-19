using MediatR;

namespace AW.Services.Customer.Application.DeleteCustomerAddress
{
    public class DeleteCustomerAddressCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public CustomerAddressDto CustomerAddress { get; set; }
    }
}