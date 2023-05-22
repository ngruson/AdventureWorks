using AutoMapper;
using AW.Services.HumanResources.Core.Entities;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.Services.HumanResources.Core.Handlers.UpdateEmployee
{
    public class Employee : IMapFrom<Entities.Employee>
    {
        public Guid ObjectId { get; set; }
        public string? Title { get; set; }

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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Employee, Employee>()
                .ReverseMap()
                .ForMember(_ => _.MaritalStatus, opt => opt.MapFrom(src => MapMaritalStatus(src.MaritalStatus)))
                .ForMember(_ => _.Gender, opt => opt.MapFrom(src => MapGender(src.Gender)))
                .ForMember(_ => _.DepartmentHistory, opt => opt.Ignore());
        }

        private static MaritalStatus? MapMaritalStatus(string? maritalStatus)
        {
            if (!string.IsNullOrEmpty(maritalStatus))
                return Entities.MaritalStatus.FromName(maritalStatus, false);

            return null;
        }

        private static Gender? MapGender(string? gender)
        {
            if (!string.IsNullOrEmpty(gender))
                return Entities.Gender.FromName(gender, false);

            return null;
        }
    }
}
