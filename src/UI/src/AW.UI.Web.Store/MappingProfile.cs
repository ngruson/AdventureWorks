using AW.SharedKernel.AutoMapper;
using AW.UI.Web.Infrastructure.ApiClients.ProductApi;
using m = AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models;
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