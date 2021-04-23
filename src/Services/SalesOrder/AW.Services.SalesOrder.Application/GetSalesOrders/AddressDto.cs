using AutoMapper;
using AW.Services.SalesOrder.Application.Common;
using AW.Services.SalesOrder.Domain;

namespace AW.Services.SalesOrder.Application.GetSalesOrders
{
    public class AddressDto : IMapFrom<Address>
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string StateProvinceCode { get; set; }
        public string CountryRegionCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Address, AddressDto>();
        }
    }
}