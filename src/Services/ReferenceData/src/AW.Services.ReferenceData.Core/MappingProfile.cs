using AW.SharedKernel.AutoMapper;
using System.Reflection;

namespace AW.Services.ReferenceData.Core
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}