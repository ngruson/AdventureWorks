using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Employee;

public class ShiftViewModel : IMapFrom<Infrastructure.Api.Employee.Handlers.GetEmployees.Shift>
{
    [Display(Name = "Shift")]
    public Guid ObjectId { get; set; }

    [Display(Name = "Shift")]
    public string? Name { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Infrastructure.Api.Employee.Handlers.GetEmployees.Shift, ShiftViewModel>();
        profile.CreateMap<Infrastructure.Api.Employee.Handlers.GetEmployee.Shift, ShiftViewModel>();
        profile.CreateMap<ShiftViewModel, Infrastructure.Api.Employee.Handlers.UpdateEmployee.Shift>()
            .ReverseMap();
    }
}
