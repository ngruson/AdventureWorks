using AW.Common.AutoMapper;
using AW.Services.SalesOrder.Application.GetSalesOrders;

namespace AW.Services.SalesOrder.Application.UnitTests
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(typeof(GetSalesOrdersQuery).Assembly);
        }
    }
}