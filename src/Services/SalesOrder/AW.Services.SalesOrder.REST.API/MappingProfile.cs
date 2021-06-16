using AW.Common.AutoMapper;
using AW.Services.SalesOrder.Application.GetSalesOrders;
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