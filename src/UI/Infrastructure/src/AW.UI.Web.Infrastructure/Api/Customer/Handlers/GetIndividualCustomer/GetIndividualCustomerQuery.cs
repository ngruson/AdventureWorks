using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetIndividualCustomer;

public class GetIndividualCustomerQuery : IRequest<IndividualCustomer?>
{
    public GetIndividualCustomerQuery(Guid objectId)
    {
        ObjectId = objectId;
    }

    public Guid ObjectId { get; set; }
}
