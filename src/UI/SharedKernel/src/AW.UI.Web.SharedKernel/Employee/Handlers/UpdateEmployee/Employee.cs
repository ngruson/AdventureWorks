using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.SharedKernel.Employee.Handlers.UpdateEmployee
{
    public class Employee : IMapFrom<GetEmployee.Employee>
    {
        public string? NationalIDNumber { get; set; }

        public string? LoginID { get; set; }

        public string? JobTitle { get; set; }

        public DateTime BirthDate { get; set; }

        public string? MaritalStatus { get; set; }

        public string? Gender { get; set; }

        public DateTime HireDate { get; set; }

        public bool Salaried { get; set; }

        public bool Current { get; set; }
    }
}
