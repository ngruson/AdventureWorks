using AutoMapper;
using AW.Application.AutoMapper;
using AW.Domain.Sales;
using System.Collections.Generic;

namespace AW.Application.Customers
{
    public class StoreDto : IMapFrom<Store>
    {
        public string Name { get; set; }
        public string SalesPerson { get; set; }
        public List<CustomerAddressDto> Addresses { get; set; }
        public List<ContactDto> Contacts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Store, StoreDto>()
                .ForMember(m => m.SalesPerson, opt => opt.MapFrom(src => src.SalesPerson.FullName))
                .ForMember(m => m.Addresses, opt => opt.MapFrom(src => src.BusinessEntityAddress))
                .ForMember(m => m.Contacts, opt => opt.MapFrom(src => src.BusinessEntityContact));
        }
    }
}