using AW.SharedKernel.AutoMapper;

namespace AW.Services.HumanResources.Core.Handlers.GetAllEmployees
{
    public class EmployeeDepartmentHistory : IMapFrom<Entities.EmployeeDepartmentHistory>
    {
        public Department? Department { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}