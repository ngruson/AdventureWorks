using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrder
{
    public class Address : IMapFrom<ValueTypes.Address>
    {
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? StateProvinceCode { get; set; }
        public string? CountryRegionCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ValueTypes.Address, Address>()
                .ForMember(_ => _.StateProvinceCode, opt => opt.MapFrom(src => src.StateProvinceCode!.Trim()));
        }
    }
}
