using AW.SharedKernel.AutoMapper;
using System.Reflection;

namespace AW.Services.Customer.WCF
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}