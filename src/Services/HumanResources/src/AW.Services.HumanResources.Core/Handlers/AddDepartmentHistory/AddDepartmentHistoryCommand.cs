using Ardalis.Result;
using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.AddDepartmentHistory
{
    public class AddDepartmentHistoryCommand : IRequest<Result>
    {
        public Guid Employee { get; set; }
        public Guid Department { get; set; }
        public Guid Shift { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
