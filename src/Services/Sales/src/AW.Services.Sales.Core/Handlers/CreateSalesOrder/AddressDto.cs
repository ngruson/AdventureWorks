using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Handlers.CreateSalesOrder
{
    public class AddressDto : IMapFrom<ValueTypes.Address>
    {
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? StateProvinceCode { get; set; }
        public string? CountryRegionCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddressDto, ValueTypes.Address>();
            profile.CreateMap<Models.Address, AddressDto>();
            profile.CreateMap<GetSalesOrder.AddressDto, AddressDto>();
        }
    }
}