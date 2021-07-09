using MediatR;

namespace AW.Services.Customer.Core.Handlers.AddCustomer
{
    public class AddCustomerCommand : IRequest<CustomerDto>
    {
        public CustomerDto Customer { get; set; }
    }
}