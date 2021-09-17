using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.SalesOrder.Core.Entities;

namespace AW.Services.SalesOrder.Core.Handlers.GetSalesOrdersForCustomer
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