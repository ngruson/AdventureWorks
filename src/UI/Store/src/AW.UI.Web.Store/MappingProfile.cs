using AW.SharedKernel.AutoMapper;
using System.Reflection;
using AW.UI.Web.SharedKernel.Interfaces.Api;

namespace AW.UI.Web.Store
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(typeof(IProductApiClient).Assembly);
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}