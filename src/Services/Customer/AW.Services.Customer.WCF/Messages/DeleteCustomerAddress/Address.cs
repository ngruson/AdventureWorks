using AutoMapper;
using AW.Services.Customer.Application.DeleteCustomerAddress;
using AW.Common.AutoMapper;

namespace AW.Services.Customer.WCF.Messages.DeleteCustomerAddress
{
    public class Address : IMapFrom<AddressDto>
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string Country { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Address, AddressDto>();
        }
    }
}