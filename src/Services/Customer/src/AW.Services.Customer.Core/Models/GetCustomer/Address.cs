using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.GetCustomer;

namespace AW.Services.Customer.Core.Models.GetCustomer
{
    public class Address : IMapFrom<AddressDto>
    {
        public string? AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        public string? PostalCode { get; set; }

        public string? City { get; set; }

        public string? StateProvinceCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddressDto, Address>();
        }
    }
}