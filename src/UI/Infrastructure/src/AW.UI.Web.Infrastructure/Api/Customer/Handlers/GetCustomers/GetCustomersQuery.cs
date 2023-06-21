using AW.SharedKernel.Interfaces;
using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomers;

public class GetCustomersQuery : IRequest<List<Customer?>?>
{
    public GetCustomersQuery(CustomerType? customerType = null)
    {
        CustomerType = customerType;
    }

    public CustomerType? CustomerType { get; set; }
}
