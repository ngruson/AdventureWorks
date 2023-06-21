using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.CreateCustomer
{
    public class StoreCustomerContact : IMapFrom<Entities.StoreCustomerContact>
    {
        public string? ContactType { get; set; }
        public Person? ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreCustomerContact, Entities.StoreCustomerContact>()
                .ForMember(_ => _.Id, opt => opt.Ignore())
                .ForMember(_ => _.ObjectId, opt => opt.Ignore())
                .ForMember(_ => _.StoreCustomerId, opt => opt.Ignore())
                .ForMember(_ => _.ContactPersonId, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
