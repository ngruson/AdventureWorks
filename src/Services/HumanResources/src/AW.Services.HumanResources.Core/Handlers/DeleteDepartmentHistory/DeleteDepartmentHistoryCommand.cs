using Ardalis.Result;
using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.DeleteDepartmentHistory
{
    public class DeleteDepartmentHistoryCommand : IRequest<Result>
    {
        public DeleteDepartmentHistoryCommand(Guid employee, Guid objectId)
        {
            Employee = employee;
            ObjectId = objectId;
        }
        public Guid Employee { get; set; }
        public Guid ObjectId { get; set; }
    }
}
