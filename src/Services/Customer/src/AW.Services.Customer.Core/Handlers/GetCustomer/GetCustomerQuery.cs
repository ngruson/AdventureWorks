using MediatR;

namespace AW.Services.Customer.Core.Handlers.GetCustomer
{
    public class GetCustomerQuery : IRequest<CustomerDto>
    {
        public GetCustomerQuery()
        {
        }
        public GetCustomerQuery(string accountNumber)
        {
            AccountNumber = accountNumber;
        }

        public string? AccountNumber { get; set; }
    }
}
