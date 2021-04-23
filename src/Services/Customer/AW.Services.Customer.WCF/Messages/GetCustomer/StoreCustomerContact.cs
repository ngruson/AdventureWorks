using AutoMapper;
using AW.Services.Customer.Application.Common;
using AW.Services.Customer.Application.GetCustomer;

namespace AW.Services.Customer.WCF.Messages.GetCustomer
{
    public class StoreCustomerContact : IMapFrom<StoreCustomerContactDto>
    {
        public string ContactType { get; set; }
        public Person ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreCustomerContactDto, StoreCustomerContact>();
        }
    }
}