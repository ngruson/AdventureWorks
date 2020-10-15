using MediatR;

namespace AW.Application.Customer.DeleteCustomerAddress
{
    public class DeleteCustomerAddressCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public string AddressTypeName { get; set; }
    }
}