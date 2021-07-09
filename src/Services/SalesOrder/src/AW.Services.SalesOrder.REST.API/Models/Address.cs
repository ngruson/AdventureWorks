using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.SalesOrder.REST.API.Models
{
    public class Address : IMapFrom<Core.Handlers.GetSalesOrders.AddressDto>
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string StateProvinceCode { get; set; }
        public string CountryRegionCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Handlers.GetSalesOrders.AddressDto, Address>();
            profile.CreateMap<Core.Handlers.GetSalesOrder.AddressDto, Address>();
        }
    }
}