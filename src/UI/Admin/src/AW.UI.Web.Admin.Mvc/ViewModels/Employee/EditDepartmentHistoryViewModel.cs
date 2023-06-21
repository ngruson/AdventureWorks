using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.AddDepartmentHistory;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.UpdateDepartmentHistory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Employee;

[BindProperties]
public class EditDepartmentHistoryViewModel : IMapFrom<AddDepartmentHistoryCommand>
{
    public EditDepartmentHistoryViewModel() { }
    
    public EditDepartmentHistoryViewModel(
        Guid employee, 
        Guid objectId,
        Guid department,
        Guid shift,
        DateTime startDate,
        DateTime? endDate,
        List<SelectListItem> departments,
        List<SelectListItem> shifts
    )
    {
        Employee = employee;
        ObjectId = objectId;
        Department = department;
        Shift = shift;
        StartDate = startDate;
        EndDate = endDate;
        Departments = departments;
        Shifts = shifts;
    }

    public EditDepartmentHistoryViewModel(
        Guid employee,
        DateTime startDate,
        List<SelectListItem> departments,
        List<SelectListItem> shifts
    )
    {
        Employee = employee;
        StartDate = startDate;
        Departments = departments;
        Shifts = shifts;
    }

    public Guid Employee { get; set; }
    public Guid ObjectId { get; set; }
    public Guid Department { get; set; }
    public Guid Shift { get; set; }

    [Required]
    [Display(Name = "Start date")]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime StartDate { get; set; }

    [Display(Name = "End date")]
    public DateTime? EndDate { get; set; }

    public List<SelectListItem>? Departments { get; set; }
    public List<SelectListItem>? Shifts { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditDepartmentHistoryViewModel, AddDepartmentHistoryCommand>();
        profile.CreateMap<EditDepartmentHistoryViewModel, UpdateDepartmentHistoryCommand>();
    }
}
