using MediatR;

namespace AW.Services.Customer.Core.Handlers.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<CustomerDto>
    {
        public CustomerDto Customer { get; set; }
    }
}