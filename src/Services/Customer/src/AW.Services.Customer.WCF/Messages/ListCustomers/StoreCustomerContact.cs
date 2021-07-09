using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.GetCustomers;

namespace AW.Services.Customer.WCF.Messages.ListCustomers
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