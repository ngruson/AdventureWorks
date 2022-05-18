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
            CreateMap<Entities.Customer, Handlers.GetSalesOrdersForCustomer.CustomerDto>()
                .IncludeAllDerived();
            CreateMap<Entities.Customer, Handlers.GetSalesOrder.CustomerDto>()
                .IncludeAllDerived();
            CreateMap<Entities.Customer, Handlers.UpdateSalesOrder.CustomerDto>()
                .IncludeAllDerived();

            CreateMap<Handlers.GetSalesOrders.CustomerDto, Models.Customer>()
                .IncludeAllDerived();
            CreateMap<Handlers.GetSalesOrdersForCustomer.CustomerDto, Models.Customer>()
                .IncludeAllDerived();
            CreateMap<Handlers.GetSalesOrder.CustomerDto, Models.Customer>()
                .IncludeAllDerived();

            CreateMap<Models.Customer, Handlers.UpdateSalesOrder.CustomerDto>()
                .IncludeAllDerived()
                .ReverseMap()
                .IncludeAllDerived();
        }
    }
}