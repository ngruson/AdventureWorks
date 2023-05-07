using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.UpdateCustomer
{
    public class Address : IMapFrom<GetCustomer.Address>
    {
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? StateProvinceCode { get; set; }
        public string? CountryRegionCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetCustomer.Address, Address>();
            profile.CreateMap<GetIndividualCustomer.Address, Address>();
            profile.CreateMap<GetStoreCustomer.Address, Address>();
        }
    }
}