using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.AddCustomer
{
    public class StoreCustomerContactDto : IMapFrom<Entities.StoreCustomerContact>
    {
        public string ContactType { get; set; }
        public PersonDto ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreCustomerContactDto, Entities.StoreCustomerContact>()
                .ForMember(_ => _.Id, opt => opt.Ignore())
                .ForMember(_ => _.StoreCustomerId, opt => opt.Ignore())
                .ForMember(_ => _.ContactPersonId, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}