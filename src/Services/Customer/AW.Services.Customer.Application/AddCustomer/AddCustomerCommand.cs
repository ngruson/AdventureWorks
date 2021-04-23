using MediatR;

namespace AW.Services.Customer.Application.AddCustomer
{
    public class AddCustomerCommand : IRequest<CustomerDto>
    {
        public CustomerDto Customer { get; set; }
    }
}