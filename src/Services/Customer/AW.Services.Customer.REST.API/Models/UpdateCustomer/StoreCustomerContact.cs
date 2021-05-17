using AutoMapper;
using AW.Common.AutoMapper;
using AW.Services.Customer.Application.UpdateCustomer;

namespace AW.Services.Customer.REST.API.Models.UpdateCustomer
{
    public class StoreCustomerContact : IMapFrom<StoreCustomerContactDto>
    {
        public string ContactType { get; set; }
        public Person ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreCustomerContact, StoreCustomerContactDto>()
                .ReverseMap();
        }
    }
}