using AutoMapper;
using AW.SharedKernel.AutoMapper;
using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.UpdateDepartmentHistory
{
    public class UpdateDepartmentHistoryCommand : IRequest, IMapFrom<Entities.EmployeeDepartmentHistory>
    {
        public string? LoginID { get; set; }
        public string? DepartmentName { get; set; }
        public string? ShiftName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateDepartmentHistoryCommand, Entities.EmployeeDepartmentHistory>();
        }
    }
}
