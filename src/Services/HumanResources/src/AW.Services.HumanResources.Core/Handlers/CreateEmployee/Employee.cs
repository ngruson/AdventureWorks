using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.Services.HumanResources.Core.Handlers.CreateEmployee
{
    public class Employee : IMapFrom<Entities.Employee>
    {
        public Employee() { }
        public Employee(NameFactory name, List<EmployeeDepartmentHistory> departmentHistory)
        {
            Name = name;
            DepartmentHistory = departmentHistory;
        }

        public string? Title { get;  set; }        
        public NameFactory? Name { get; set; }
        public string? Suffix { get; set; }
        public string? NationalIDNumber { get; set; }

        public string? LoginID { get; set; }

        public string? JobTitle { get; set; }

        public DateTime BirthDate { get; set; }
        public string? MaritalStatus { get; set; }

        public string? Gender { get; set; }

        public DateTime HireDate { get; set; }

        public bool Salaried { get; set; }

        public bool Current { get; set; }

        public List<EmployeeDepartmentHistory> DepartmentHistory { get; internal set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Employee, Employee>()
                .ReverseMap()
                .ForMember(_ => _.MaritalStatus, opt => opt.MapFrom(src => Entities.MaritalStatus.FromName(src.MaritalStatus, false)))
                .ForMember(_ => _.Gender, opt => opt.MapFrom(src => Entities.Gender.FromName(src.Gender, false)));
        }
    }
}
