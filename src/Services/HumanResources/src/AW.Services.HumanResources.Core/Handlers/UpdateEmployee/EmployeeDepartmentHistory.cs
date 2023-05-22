using AW.SharedKernel.AutoMapper;

namespace AW.Services.HumanResources.Core.Handlers.UpdateEmployee
{
    public class EmployeeDepartmentHistory : IMapFrom<Entities.EmployeeDepartmentHistory>
    {
        public Guid ObjectId { get; set; }
        public Department? Department { get; set; }
        public Shift? Shift { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
