using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Employee
{
    public class EmployeeViewModel : IMapFrom<SharedKernel.Employee.Handlers.GetEmployees.Employee>
    {
        public NameFactory? Name { get; set; }
        public string? NationalIDNumber { get; set; }

        public string? LoginID { get; set; }

        public string? JobTitle { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime HireDate { get; set; }

        public bool Salaried { get; set; }

        public bool Current { get; set; }

        public List<EmployeeDepartmentHistoryViewModel> DepartmentHistory { get; set; } = new();
    }
}
