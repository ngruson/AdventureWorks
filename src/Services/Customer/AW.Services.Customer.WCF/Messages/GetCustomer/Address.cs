using AutoMapper;
using AW.Common.AutoMapper;
using AW.Services.Customer.Application.GetCustomer;

namespace AW.Services.Customer.WCF.Messages.GetCustomer
{
    public class Address : IMapFrom<AddressDto>
    {
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string StateProvinceCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddressDto, Address>();
        }
    }
}