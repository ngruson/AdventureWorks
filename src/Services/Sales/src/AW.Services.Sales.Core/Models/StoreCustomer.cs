using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Models
{
    public class StoreCustomer : Customer, IMapFrom<Handlers.GetSalesOrders.StoreCustomerDto>
    {
        public string? Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Handlers.GetSalesOrders.StoreCustomerDto, StoreCustomer>();
            profile.CreateMap<Handlers.GetSalesOrdersForCustomer.StoreCustomerDto, StoreCustomer>();
            profile.CreateMap<Handlers.GetSalesOrder.StoreCustomerDto, StoreCustomer>();
            profile.CreateMap<StoreCustomer, Handlers.UpdateSalesOrder.StoreCustomerDto>()
                .ReverseMap();
        }
    }
}