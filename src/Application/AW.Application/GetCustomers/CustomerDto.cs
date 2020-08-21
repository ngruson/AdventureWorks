using AutoMapper;
using AW.Application.Common.Mappings;
using AW.Domain.Sales;

namespace AW.Application.GetCustomers
{
    public class CustomerDto : IMapFrom<Customer>
    {
        public string AccountNumber { get; set; }
        public string SalesTerritoryName { get; set; }
        public PersonDto Person { get; set; }
        public StoreDto Store { get; set; }

        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<Customer, CustomerDto>();            
        }
    }
}