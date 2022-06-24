using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models.UpdateCustomer
{
    public class Address : IMapFrom<GetCustomer.Address>
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string StateProvinceCode { get; set; }
        public string CountryRegionCode { get; set; }
    }
}