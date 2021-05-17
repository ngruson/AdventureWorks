using AW.Common.AutoMapper;
using System.Reflection;

namespace AW.Services.SalesOrder.REST.API
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}