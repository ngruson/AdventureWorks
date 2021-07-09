using MediatR;

namespace AW.Services.Customer.Core.Handlers.GetCustomer
{
    public class GetCustomerQuery : IRequest<CustomerDto>
    {
        public string AccountNumber { get; set; }
    }
}