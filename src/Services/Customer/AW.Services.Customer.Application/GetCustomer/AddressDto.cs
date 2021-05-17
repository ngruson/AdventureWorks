using AutoMapper;
using AW.Common.AutoMapper;

namespace AW.Services.Customer.Application.GetCustomer
{
    public class AddressDto : IMapFrom<Domain.Address>
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string StateProvinceCode { get; set; }
        public string CountryRegionCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Address, AddressDto>()
                .ForMember(m => m.StateProvinceCode, opt => opt.MapFrom(src => src.StateProvinceCode.Trim()));
        }
    }
}