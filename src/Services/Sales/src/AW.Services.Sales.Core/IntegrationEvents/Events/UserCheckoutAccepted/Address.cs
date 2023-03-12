using AutoMapper;
using AW.Services.Sales.Core.Handlers.CreateSalesOrder;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.IntegrationEvents.Events.UserCheckoutAccepted
{
    public class Address : IMapFrom<AddressDto>
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
                .ReverseMap();
        }
    }
}
