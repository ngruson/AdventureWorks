using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Employee
{
    public class EditEmployeeEmployeeViewModel : IMapFrom<SharedKernel.Employee.Handlers.UpdateEmployee.Employee>
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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.Employee.Handlers.UpdateEmployee.Employee, EditEmployeeEmployeeViewModel>()
                .ReverseMap();
        }
    }
}
