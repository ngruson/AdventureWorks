using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.Services.HumanResources.Core.Handlers.GetAllEmployees
{
    public class Employee : IMapFrom<Entities.Employee>
    {
        public Employee() { }
        public Employee(NameFactory name, List<EmployeeDepartmentHistory> departmentHistory)
        {
            Name = name;
            DepartmentHistory = departmentHistory;
        }

        public NameFactory? Name { get; internal set; }
        public string? NationalIDNumber { get; internal set; }

        public string? LoginID { get; internal set; }

        public string? JobTitle { get; internal set; }

        public DateTime BirthDate { get; internal set; }

        public DateTime HireDate { get; internal set; }

        public bool Salaried { get; internal set; }

        public bool Current { get; internal set; }

        public List<EmployeeDepartmentHistory> DepartmentHistory { get; internal set; } = new();
    }
}