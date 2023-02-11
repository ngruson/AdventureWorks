using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.Services.Sales.Core.Models
{
    public class SalesOrderSalesPerson : IMapFrom<Handlers.GetSalesOrders.SalesPersonDto>
    {
        public NameFactory? Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Handlers.GetSalesOrders.SalesPersonDto, SalesOrderSalesPerson>();
            profile.CreateMap<Handlers.GetSalesOrdersForCustomer.SalesPersonDto, SalesOrderSalesPerson>();
            profile.CreateMap<Handlers.GetSalesOrder.SalesPersonDto, SalesOrderSalesPerson>();
            profile.CreateMap<SalesOrderSalesPerson, Handlers.UpdateSalesOrder.SalesPersonDto>()
                .ReverseMap();
        }
    }
}