using System.Reflection;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.REST.API
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());            
        }
    }
}