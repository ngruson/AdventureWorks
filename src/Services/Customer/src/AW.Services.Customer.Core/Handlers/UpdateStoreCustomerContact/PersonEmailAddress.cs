using AutoMapper;
using AW.Services.SharedKernel.ValueTypes;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.UpdateStoreCustomerContact
{
    public class PersonEmailAddress : IMapFrom<Entities.PersonEmailAddress>
    {
        public EmailAddress? EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.PersonEmailAddress, PersonEmailAddress>()
                .ReverseMap();
        }
    }
}
