using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.UpdateCustomer;

namespace AW.Services.Customer.WCF.Messages.UpdateCustomer
{
    public class StoreCustomerContact : IMapFrom<StoreCustomerContactDto>
    {
        public string ContactType { get; set; }
        public PersonDto ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreCustomerContact, StoreCustomerContactDto>();
        }
    }
}