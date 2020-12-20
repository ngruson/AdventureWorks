using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Domain.Sales;
using System.Collections.Generic;

namespace AW.Core.Application.Customer.GetCustomer
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