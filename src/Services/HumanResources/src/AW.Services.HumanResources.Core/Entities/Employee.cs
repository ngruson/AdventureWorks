using AW.Services.SharedKernel.Interfaces;

namespace AW.Services.HumanResources.Core.Entities
{
    public class Employee : Person, IAggregateRoot
    {
        public string? NationalIDNumber { get; set; }

        public string? LoginID { get; set; }

        public string? JobTitle { get; set; }

        public DateTime BirthDate { get; set; }

        public MaritalStatus? MaritalStatus { get; set; }

        public Gender? Gender { get; set; }

        public DateTime HireDate { get; set; }

        public bool Salaried { get; set; }

        public bool Current { get; set; }

        public List<EmployeeDepartmentHistory> DepartmentHistory { get; internal set; } = new();
    }
}