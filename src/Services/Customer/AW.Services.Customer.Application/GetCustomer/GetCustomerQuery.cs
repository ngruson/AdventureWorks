using MediatR;

namespace AW.Services.Customer.Application.GetCustomer
{
    public class GetCustomerQuery : IRequest<CustomerDto>
    {
        public string AccountNumber { get; set; }
    }
}