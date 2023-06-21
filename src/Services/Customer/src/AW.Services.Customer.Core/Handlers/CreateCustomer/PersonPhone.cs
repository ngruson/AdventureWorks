using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.CreateCustomer
{
    public class PersonPhone : IMapFrom<Entities.PersonPhone>
    {
        public string? PhoneNumberType { get; set; }
        public string? PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonPhone, Entities.PersonPhone>()
                .ForMember(_ => _.Id, opt => opt.Ignore())
                .ForMember(_ => _.ObjectId, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
