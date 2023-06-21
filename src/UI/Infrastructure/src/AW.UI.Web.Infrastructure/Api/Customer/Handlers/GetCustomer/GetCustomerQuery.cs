using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomer;

public class GetCustomerQuery : IRequest<Customer?>
{
    public GetCustomerQuery(Guid objectId)
    {
        ObjectId = objectId;
    }

    public Guid ObjectId { get; private init; }
}
