using AW.SharedKernel.ValueTypes;

namespace AW.UI.Web.Infrastructure.Api.Employee.Handlers.GetEmployee
{
    public class Employee
    {
        public NameFactory? Name { get; set; }
        public string? NationalIDNumber { get; set; }

        public string? LoginID { get; set; }

        public string? JobTitle { get; set; }

        public DateTime BirthDate { get; set; }
        public string? MaritalStatus { get; set; }

        public string? Gender { get; set; }

        public DateTime HireDate { get; set; }

        public bool Salaried { get; set; }

        public bool Current { get; set; }

        public List<EmployeeDepartmentHistory> DepartmentHistory { get; set; } = new();
    }
}
