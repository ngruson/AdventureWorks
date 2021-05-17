using AutoMapper;
using AW.Common.AutoMapper;
using AW.Services.SalesOrder.Application.GetSalesOrder;

namespace AW.Services.SalesOrder.WCF.Messages.GetSalesOrder
{
    public class Address : IMapFrom<AddressDto>
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string StateProvinceCode { get; set; }
        public string CountryRegionCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddressDto, Address>();
        }
    }
}