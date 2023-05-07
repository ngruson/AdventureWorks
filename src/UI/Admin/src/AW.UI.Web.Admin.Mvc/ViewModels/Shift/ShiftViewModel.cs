using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Shift
{
    public class ShiftViewModel : IMapFrom<Infrastructure.Api.Shift.Handlers.GetShifts.Shift>
    {
        public Guid ObjectId { get; set; }

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
            profile.CreateMap<Infrastructure.Api.Shift.Handlers.GetShifts.Shift, ShiftViewModel>();
            profile.CreateMap<Infrastructure.Api.Shift.Handlers.GetShift.Shift, ShiftViewModel>();
            profile.CreateMap<ShiftViewModel, Infrastructure.Api.Shift.Handlers.CreateShift.Shift>();
            profile.CreateMap<ShiftViewModel, Infrastructure.Api.Shift.Handlers.UpdateShift.Shift>();
        }
    }
}
