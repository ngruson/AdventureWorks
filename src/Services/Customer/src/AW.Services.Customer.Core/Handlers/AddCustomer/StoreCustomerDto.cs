using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System.Collections.Generic;

namespace AW.Services.Customer.Core.Handlers.AddCustomer
{
    public class StoreCustomerDto : CustomerDto, IMapFrom<Entities.StoreCustomer>
    {
        public string Name { get; set; }
        public string SalesPerson { get; set; }
        public List<StoreCustomerContactDto> Contacts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreCustomerDto, Entities.StoreCustomer>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.SalesOrders, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}