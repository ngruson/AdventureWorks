using AW.Services.HumanResources.Core.Entities;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.Services.HumanResources.Core.Handlers.GetEmployee
{
    public class Employee : IMapFrom<Entities.Employee>
    {
        public Employee() { }
        public Employee(NameFactory name, List<EmployeeDepartmentHistory> departmentHistory)
        {
            Name = name;
            DepartmentHistory = departmentHistory;
        }

        public string Title { get; internal set; }        
        public NameFactory? Name { get; internal set; }
        public string Suffix { get; internal set; }
        public string? NationalIDNumber { get; internal set; }

        public string? LoginID { get; internal set; }

        public string? JobTitle { get; internal set; }

        public DateTime BirthDate { get; internal set; }

        public MaritalStatus? MaritalStatus { get; set; }

        public Gender? Gender { get; set; }

        public DateTime HireDate { get; set; }

        public bool Salaried { get; set; }

        public bool Current { get; set; }

        public List<EmployeeDepartmentHistory> DepartmentHistory { get; internal set; } = new();
    }
}