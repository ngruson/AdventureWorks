using AutoMapper;
using AW.Services.Customer.Core.Handlers.UpdateCustomer;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Models.UpdateCustomer
{
    public class StoreCustomerContact : IMapFrom<StoreCustomerContactDto>
    {
        public string? ContactType { get; set; }
        public Person? ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreCustomerContact, StoreCustomerContactDto>()
                .ReverseMap();
        }
    }
}