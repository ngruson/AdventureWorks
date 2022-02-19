using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.UpdateCustomerAddress
{
    public class AddressDto : IMapFrom<Entities.Address>
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string StateProvinceCode { get; set; }
        public string CountryRegionCode { get; set; }
    }
}