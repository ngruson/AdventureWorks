using AW.SharedKernel.AutoMapper;
using System.Reflection;

namespace AW.Services.SalesPerson.REST.API
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}