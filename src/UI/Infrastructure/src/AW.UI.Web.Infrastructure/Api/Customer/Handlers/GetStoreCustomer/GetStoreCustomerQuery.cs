using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetStoreCustomer;

public class GetStoreCustomerQuery : IRequest<StoreCustomer?>
{
    public GetStoreCustomerQuery(Guid objectId)
    {
        ObjectId = objectId;
    }

    public Guid ObjectId { get; set; }
}
