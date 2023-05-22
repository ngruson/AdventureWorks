using Ardalis.Result;
using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.GetEmployee
{
    public class GetEmployeeQuery : IRequest<Result<Employee>>
    {
        public GetEmployeeQuery()
        {
        }
        public GetEmployeeQuery(Guid objectId)
        {
            ObjectId = objectId;
        }

        public Guid ObjectId { get; set; }
    }
}
