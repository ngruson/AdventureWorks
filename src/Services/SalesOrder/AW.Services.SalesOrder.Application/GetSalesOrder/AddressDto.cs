using AutoMapper;
using AW.Services.SalesOrder.Application.Common;

namespace AW.Services.SalesOrder.Application.GetSalesOrder
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
            profile.CreateMap<Domain.Address, AddressDto>();
        }
    }
}