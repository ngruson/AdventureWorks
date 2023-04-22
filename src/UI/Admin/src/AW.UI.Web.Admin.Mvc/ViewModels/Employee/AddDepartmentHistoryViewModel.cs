using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.UI.Web.SharedKernel.Employee.Handlers.AddDepartmentHistory;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Employee
{
    public class AddDepartmentHistoryViewModel : IMapFrom<AddDepartmentHistoryCommand>
    {
        public string? LoginID { get; set; }

        [Display(Name = "Department name")]
        public string? DepartmentName { get; set; }

        [Display(Name = "Shift name")]
        public string? ShiftName { get; set; }

        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End date")]
        public DateTime? EndDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddDepartmentHistoryViewModel, AddDepartmentHistoryCommand>();
        }
    }
}
