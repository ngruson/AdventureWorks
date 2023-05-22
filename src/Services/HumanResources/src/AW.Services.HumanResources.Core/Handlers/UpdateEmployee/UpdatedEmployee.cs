using AutoMapper;
using AW.Services.HumanResources.Core.Entities;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.Services.HumanResources.Core.Handlers.UpdateEmployee
{
    public class UpdatedEmployee : IMapFrom<Entities.Employee>
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

        public List<EmployeeDepartmentHistory> DepartmentHistory { get; internal set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Employee, UpdatedEmployee>();
        }
    }
}
