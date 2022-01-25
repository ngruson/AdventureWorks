using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Models
{
    public class Customer : IMapFrom<Handlers.GetSalesOrders.CustomerDto>
    {
        public CustomerType CustomerType { get; set; }
        public string CustomerNumber { get; set; }
        public string FullName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Handlers.GetSalesOrders.CustomerDto, Customer>();
            profile.CreateMap<Handlers.GetSalesOrder.CustomerDto, Customer>();
            profile.CreateMap<Handlers.GetSalesOrdersForCustomer.CustomerDto, Customer>();
        }
    }
}