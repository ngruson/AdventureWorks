using AW.SharedKernel.AutoMapper;

namespace AW.Services.HumanResources.Core.AutoMapper
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(typeof(Handlers.GetEmployees.GetEmployeesQuery).Assembly);
        }
    }
}
