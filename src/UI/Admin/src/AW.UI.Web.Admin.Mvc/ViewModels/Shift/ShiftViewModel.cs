using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Shift
{
    public class ShiftViewModel : IMapFrom<SharedKernel.Shift.Handlers.GetShifts.Shift>
    {
        [Display(Name = "Name")]
        [Required]
        public string? Name { get; set; }

        [Display(Name = "Start time")]
        [Required]
        public TimeSpan StartTime { get; set; }

        [Display(Name = "End time")]
        [Required]
        public TimeSpan EndTime { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.Shift.Handlers.GetShifts.Shift, ShiftViewModel>();
            profile.CreateMap<SharedKernel.Shift.Handlers.GetShift.Shift, ShiftViewModel>();
            profile.CreateMap<ShiftViewModel, SharedKernel.Shift.Handlers.CreateShift.Shift>();
            profile.CreateMap<ShiftViewModel, SharedKernel.Shift.Handlers.UpdateShift.Shift>();
        }
    }
}
