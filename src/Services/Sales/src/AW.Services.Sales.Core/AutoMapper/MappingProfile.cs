using AW.SharedKernel.AutoMapper;
using System.Reflection;

namespace AW.Services.Sales.Core.AutoMapper
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

            CreateMap<Entities.Customer, Handlers.GetSalesOrders.CustomerDto>()
                .IncludeAllDerived();
            CreateMap<Entities.Customer, Handlers.GetSalesOrder.CustomerDto>()
                .IncludeAllDerived();
        }
    }
}