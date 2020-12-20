using AutoMapper;
using AW.Core.Application.AutoMapper;
using System.Collections.Generic;

namespace AW.Core.Application.Customer.UpdateCustomer
{
    public class CustomerDto : IMapFrom<Domain.Sales.Customer>
    {
        public string AccountNumber { get; set; }
        public string SalesTerritoryName { get; set; }
        public PersonCustomerDto Person { get; set; }
        public StoreCustomerDto Store { get; set; }
        public List<SalesOrderDto> SalesOrders { get; set; }

        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Sales.Customer, CustomerDto>()
                .ReverseMap();
        }
    }
}