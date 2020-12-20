using AW.Core.Application.AutoMapper;
using System.Reflection;

namespace AW.SalesOrderService
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}