using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Employee;

public class EmployeeViewModel : IMapFrom<Infrastructure.Api.Employee.Handlers.GetEmployees.Employee>
{
    public Guid ObjectId { get; set; }

    [Display(Name = "Title")]
    public string? Title { get; set; }

    public EmployeeNameViewModel? Name { get; set; }

    [Display(Name = "Suffix")]
    public string? Suffix { get; set; }

    [Display(Name = "National ID number")]
    [Required]
    public string? NationalIDNumber { get; set; }

    [Display(Name = "Login ID")]
    [Required]
    public string? LoginID { get; set; }

    [Display(Name = "Job title")]
    [Required]
    public string? JobTitle { get; set; }

    [Display(Name = "Birth date")]
    [Required]
    public DateTime BirthDate { get; set; }

    [Display(Name = "Marital status")]
    [Required]
    public string? MaritalStatus { get; set; }

    [Display(Name = "Gender")]
    [Required]
    public string? Gender { get; set; }

    [Display(Name = "Hire date")]
    [Required]
    public DateTime HireDate { get; set; }

    public bool Salaried { get; set; }

    public bool Current { get; set; }

    public List<EmployeeDepartmentHistoryViewModel> DepartmentHistory { get; set; } = new();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Infrastructure.Api.Employee.Handlers.GetEmployees.Employee, EmployeeViewModel>();
        profile.CreateMap<Infrastructure.Api.Employee.Handlers.GetEmployee.Employee, EmployeeViewModel>();
        profile.CreateMap<EmployeeViewModel, Infrastructure.Api.Employee.Handlers.CreateEmployee.Employee>();
        profile.CreateMap<EmployeeViewModel, Infrastructure.Api.Employee.Handlers.UpdateEmployee.Employee>();
        profile.CreateMap<Infrastructure.Api.Employee.Handlers.UpdateEmployee.UpdatedEmployee, EmployeeViewModel>();
    }
}
