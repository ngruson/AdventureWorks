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
                .ForMember(m => m.SalesOrderCount, opt => opt.MapFrom(src => src.SalesOrders.Count))
                .IncludeAllDerived();
            CreateMap<Entities.Customer, Handlers.GetSalesOrdersForCustomer.CustomerDto>()
                .ForMember(m => m.SalesOrderCount, opt => opt.MapFrom(src => src.SalesOrders.Count))
                .IncludeAllDerived();
            CreateMap<Entities.Customer, Handlers.GetSalesOrder.Customer>()
                .ForMember(m => m.SalesOrderCount, opt => opt.MapFrom(src => src.SalesOrders.Count))
                .IncludeAllDerived();
            CreateMap<Entities.Customer, Handlers.UpdateSalesOrder.Customer>()
                .IncludeAllDerived();
        }
    }
}
