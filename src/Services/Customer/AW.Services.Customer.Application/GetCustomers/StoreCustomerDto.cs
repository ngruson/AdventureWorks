using AutoMapper;
using AW.Common.AutoMapper;
using AW.Services.Customer.Domain;
using System.Collections.Generic;

namespace AW.Services.Customer.Application.GetCustomers
{
    public class StoreCustomerDto : CustomerDto, IMapFrom<StoreCustomer>
    {
        public override CustomerType CustomerType => CustomerType.Store;
        public string Name { get; set; }
        public string SalesPerson { get; set; }
        public List<StoreCustomerContactDto> Contacts { get; set; } = new List<StoreCustomerContactDto>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreCustomer, StoreCustomerDto>();
        }
    }
}