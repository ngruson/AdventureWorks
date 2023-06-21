using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.UpdateCustomer;

public class UpdateCustomerCommand : IRequest
{
    public UpdateCustomerCommand(Customer customer)
    {
        Customer = customer;
    }

    public Customer Customer { get; set; }
}
