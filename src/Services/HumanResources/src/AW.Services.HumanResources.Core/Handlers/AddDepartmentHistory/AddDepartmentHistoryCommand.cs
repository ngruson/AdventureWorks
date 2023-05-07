using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.AddDepartmentHistory
{
    public class AddDepartmentHistoryCommand : IRequest
    {
        public string? LoginID { get; set; }
        public string? DepartmentName { get; set; }
        public Guid Shift { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
