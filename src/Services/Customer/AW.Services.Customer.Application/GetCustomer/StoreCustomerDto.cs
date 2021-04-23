using AutoMapper;
using AW.Services.Customer.Application.Common;
using AW.Services.Customer.Domain;
using System.Collections.Generic;

namespace AW.Services.Customer.Application.GetCustomer
{
    public class StoreCustomerDto : CustomerDto, IMapFrom<StoreCustomer>
    {
        public override CustomerType CustomerType => CustomerType.Store;
        public string Name { get; set; }
        public string SalesPerson { get; set; }
        public List<StoreCustomerContactDto> Contacts { get; set; } = new List<StoreCustomerContactDto>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.StoreCustomer, StoreCustomerDto>();
        }
    }
}