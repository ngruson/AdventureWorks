using AW.Services.SalesOrder.Core.Handlers.GetSalesOrders;
using AW.SharedKernel.AutoMapper;
using System.Reflection;

namespace AW.Services.SalesOrder.REST.API
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
            ApplyMappingsFromAssembly(typeof(GetSalesOrdersQuery).Assembly);
        }
    }
}