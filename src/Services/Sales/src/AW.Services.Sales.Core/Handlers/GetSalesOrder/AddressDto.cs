using AutoMapper;
using AW.Services.Sales.Core.ValueTypes;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrder
{
    public class AddressDto : IMapFrom<Address>
    {
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? StateProvinceCode { get; set; }
        public string? CountryRegionCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Address, AddressDto>()
                .ForMember(_ => _.StateProvinceCode, opt => opt.MapFrom(src => src.StateProvinceCode.Trim()));
        }
    }
}