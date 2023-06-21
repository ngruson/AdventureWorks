using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.GetCustomer
{
    public class PersonEmailAddress : IMapFrom<Entities.PersonEmailAddress>
    {
        public Guid ObjectId { get; set; }
        public string? EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.PersonEmailAddress, PersonEmailAddress>()
                .ForMember(m => m.EmailAddress, opt => opt.MapFrom(src => src.EmailAddress!.Value));
        }
    }
}
