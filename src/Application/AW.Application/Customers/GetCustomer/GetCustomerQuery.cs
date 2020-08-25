using AW.Application.Customers;
using MediatR;

namespace AW.Application.GetCustomer
{
    public class GetCustomerQuery : IRequest<CustomerDto>
    {
        public string AccountNumber { get; set; }
    }
}