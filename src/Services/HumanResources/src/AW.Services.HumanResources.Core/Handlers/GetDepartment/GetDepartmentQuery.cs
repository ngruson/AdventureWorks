using Ardalis.Result;
using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.GetDepartment
{
    public class GetDepartmentQuery : IRequest<Result<Department>>
    {
        public GetDepartmentQuery()
        {
        }
        public GetDepartmentQuery(Guid objectId)
        {
            ObjectId = objectId;
        }

        public Guid ObjectId { get; set; }
    }
}
