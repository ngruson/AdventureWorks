using MediatR;

namespace AW.Services.Customer.Application.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<CustomerDto>
    {
        public CustomerDto Customer { get; set; }
    }
}