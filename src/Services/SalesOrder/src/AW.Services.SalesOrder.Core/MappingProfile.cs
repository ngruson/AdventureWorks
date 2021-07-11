using AW.SharedKernel.AutoMapper;
using System.Reflection;

namespace AW.Services.SalesOrder.Core
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}