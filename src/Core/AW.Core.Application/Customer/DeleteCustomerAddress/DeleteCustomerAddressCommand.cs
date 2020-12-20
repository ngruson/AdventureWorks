using MediatR;

namespace AW.Core.Application.Customer.DeleteCustomerAddress
{
    public class DeleteCustomerAddressCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public string AddressTypeName { get; set; }
    }
}