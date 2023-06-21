using Ardalis.Result;
using MediatR;

namespace AW.Services.Customer.Core.Handlers.GetCustomer
{
    public class GetCustomerQuery : IRequest<Result<Customer>>
    {
        public GetCustomerQuery()
        {
        }
        public GetCustomerQuery(Guid objectId)
        {
            ObjectId = objectId;
        }

        public Guid ObjectId { get; set; }
    }
}
