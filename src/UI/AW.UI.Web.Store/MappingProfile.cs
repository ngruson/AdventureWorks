using AW.Common.AutoMapper;
using AW.UI.Web.Common.ApiClients.ProductApi;
using System.Reflection;

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