using Ardalis.Result;
using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.DeleteDepartmentHistory
{
    public class DeleteDepartmentHistoryCommand : IRequest<Result>
    {
        public string? LoginID { get; set; }
        public Guid ObjectId { get; set; }
    }
}
