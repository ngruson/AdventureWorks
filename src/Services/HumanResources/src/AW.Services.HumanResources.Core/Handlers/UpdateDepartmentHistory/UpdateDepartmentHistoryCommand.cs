using AutoMapper;
using AW.SharedKernel.AutoMapper;
using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.UpdateDepartmentHistory
{
    public class UpdateDepartmentHistoryCommand : IRequest, IMapFrom<Entities.EmployeeDepartmentHistory>
    {
        public Guid ObjectId { get; set; }
        public string? LoginID { get; set; }
        public string? DepartmentName { get; set; }
        public Guid Shift { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateDepartmentHistoryCommand, Entities.EmployeeDepartmentHistory>()
                .ForMember(_ => _.Id, opt => opt.Ignore())
                .ForMember(_ => _.EmployeeID, opt => opt.Ignore())
                .ForMember(_ => _.DepartmentID, opt => opt.Ignore())
                .ForMember(_ => _.ShiftID, opt => opt.Ignore())
                .ForMember(_ => _.Department, opt => opt.Ignore())
                .ForMember(_ => _.Shift, opt => opt.Ignore());
        }
    }
}
