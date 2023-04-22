using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Employee
{
    public class EmployeeViewModel : IMapFrom<SharedKernel.Employee.Handlers.GetEmployees.Employee>
    {
        public EmployeeNameViewModel? Name { get; set; }

        [Display(Name = "National ID number")]
        public string? NationalIDNumber { get; set; }

        [Display(Name = "Login ID")]
        public string? LoginID { get; set; }

        [Display(Name = "Job title")]
        public string? JobTitle { get; set; }

        [Display(Name = "Birth date")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Marital status")]
        public string? MaritalStatus { get; set; }

        [Display(Name = "Gender")]
        public string? Gender { get; set; }

        [Display(Name = "Hire date")]
        public DateTime HireDate { get; set; }

        public bool Salaried { get; set; }

        public bool Current { get; set; }

        public List<EmployeeDepartmentHistoryViewModel> DepartmentHistory { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.Employee.Handlers.GetEmployees.Employee, EmployeeViewModel>();
            profile.CreateMap<SharedKernel.Employee.Handlers.GetEmployee.Employee, EmployeeViewModel>();
        }
    }
}
