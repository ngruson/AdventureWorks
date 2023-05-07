using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<Customer>
    {
        public UpdateCustomerCommand(string? accountNumber, Customer? customer)
        {
            AccountNumber = accountNumber;
            Customer = customer;
        }

        public string? AccountNumber { get; set; }
        public Customer? Customer { get; set; }
    }
}