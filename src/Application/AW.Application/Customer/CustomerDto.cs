using AutoMapper;
using AW.Application.AutoMapper;
using System.Collections.Generic;

namespace AW.Application.Customers
{
    public class CustomerDto : IMapFrom<Domain.Sales.Customer>
    {
        public string AccountNumber { get; set; }
        public string SalesTerritoryName { get; set; }
        public PersonDto Person { get; set; }
        public StoreDto Store { get; set; }
        public List<SalesOrderDto> SalesOrders { get; set; }

        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Sales.Customer, CustomerDto>();
        }
    }
}