using AW.SharedKernel.AutoMapper;
using System.Reflection;

namespace AW.Services.Product.Core.AutoMapper
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}