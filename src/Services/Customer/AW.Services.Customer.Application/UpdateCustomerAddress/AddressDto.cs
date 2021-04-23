using AutoMapper;
using AW.Services.Customer.Application.Common;

namespace AW.Services.Customer.Application.UpdateCustomerAddress
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
            profile.CreateMap<AddressDto, Domain.Address>()
                .ForMember(m => m.Id, opt => opt.Ignore());
        }
    }
}