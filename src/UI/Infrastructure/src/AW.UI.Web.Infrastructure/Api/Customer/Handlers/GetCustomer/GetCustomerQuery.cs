using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomer
{
    public class GetCustomerQuery : IRequest<Customer?>
    {
        public GetCustomerQuery(string? accountNumber)
        {
            AccountNumber = accountNumber;
        }

        public string? AccountNumber { get; set; }
    }
}