using MediatR;

namespace AW.Application.Customer.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<CustomerDto>
    {
        public CustomerDto Customer { get; set; }
    }
}