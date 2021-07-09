using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.AddIndividualCustomerPhone
{
    public class PhoneDto : IMapFrom<Entities.PersonPhone>
    {
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PhoneDto, Entities.PersonPhone>()
                .ForMember(m => m.Id, opt => opt.Ignore());
        }
    }
}