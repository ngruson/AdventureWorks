using MediatR;

namespace AW.Core.Application.Customer.GetCustomer
{
    public class GetCustomerQuery : IRequest<CustomerDto>
    {
        public string AccountNumber { get; set; }
    }
}