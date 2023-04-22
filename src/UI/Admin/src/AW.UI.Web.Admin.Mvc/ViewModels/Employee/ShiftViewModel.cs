using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Employee
{
    public class ShiftViewModel : IMapFrom<SharedKernel.Employee.Handlers.GetEmployees.Shift>
    {
        [Display(Name = "Shift")]
        public string? Name { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.Employee.Handlers.GetEmployees.Shift, ShiftViewModel>();
            profile.CreateMap<SharedKernel.Employee.Handlers.GetEmployee.Shift, ShiftViewModel>();
        }
    }
}
