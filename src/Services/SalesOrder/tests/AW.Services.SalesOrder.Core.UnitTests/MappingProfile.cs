using AW.Services.SalesOrder.Core.Handlers.GetSalesOrders;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.SalesOrder.Core.UnitTests
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(typeof(GetSalesOrdersQuery).Assembly);
        }
    }
}