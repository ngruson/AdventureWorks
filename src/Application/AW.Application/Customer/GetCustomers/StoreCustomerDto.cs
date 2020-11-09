using AutoMapper;
using AW.Application.AutoMapper;
using AW.Domain.Sales;
using System.Collections.Generic;

namespace AW.Application.Customer.GetCustomers
{
    public class StoreCustomerDto : IMapFrom<Store>
    {
        public string Name { get; set; }
        public SalesPersonDto SalesPerson { get; set; }
        public List<CustomerAddressDto> Addresses { get; set; }
        public List<CustomerContactDto> Contacts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Store, StoreCustomerDto>()
                .ForMember(m => m.Addresses, opt => opt.MapFrom(src => src.BusinessEntityAddresses))
                .ForMember(m => m.Contacts, opt => opt.MapFrom(src => src.BusinessEntityContacts))
                .ReverseMap();
        }
    }
}