using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Models
{
    public class Address : IMapFrom<Handlers.GetSalesOrders.AddressDto>
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string StateProvinceCode { get; set; }
        public string CountryRegionCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Handlers.GetSalesOrders.AddressDto, Address>();
            profile.CreateMap<Handlers.GetSalesOrder.AddressDto, Address>();
            profile.CreateMap<Address, Handlers.UpdateSalesOrder.AddressDto>()
                .ReverseMap();
            profile.CreateMap<Address, ValueTypes.Address>();
        }
    }
}