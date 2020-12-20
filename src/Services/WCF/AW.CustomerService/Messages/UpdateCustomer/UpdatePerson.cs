using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Application.Customer.UpdateCustomer;

namespace AW.CustomerService.Messages.UpdateCustomer
{
    public class UpdatePerson : IMapFrom<PersonCustomerDto>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Suffix { get; set; }
        public EmailPromotion EmailPromotion { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdatePerson, PersonCustomerDto>()
                .ForMember(m => m.Addresses, opt => opt.Ignore())
                .ForMember(m => m.ContactInfo, opt => opt.Ignore());
        }
    }
}