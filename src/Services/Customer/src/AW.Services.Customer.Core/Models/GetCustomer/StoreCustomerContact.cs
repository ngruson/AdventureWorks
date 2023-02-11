using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.GetCustomer;

namespace AW.Services.Customer.Core.Models.GetCustomer
{
    public class StoreCustomerContact : IMapFrom<StoreCustomerContactDto>
    {
        public string? ContactType { get; set; }
        public Person? ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreCustomerContactDto, StoreCustomerContact>();
        }
    }
}