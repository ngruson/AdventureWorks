using AutoMapper;
using AW.Application.AutoMapper;
using AW.Domain.Person;
using System.Collections.Generic;
using System.Linq;

namespace AW.Application.Customer.GetCustomer
{
    public class PersonCustomerDto : IMapFrom<Person>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Suffix { get; set; }
        public EmailPromotion EmailPromotion { get; set; }
        public List<CustomerAddressDto> Addresses { get; set; }
        public List<ContactInfoDto> ContactInfo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Person, PersonCustomerDto>()
                .ForMember(m => m.Addresses, opt => opt.MapFrom(src => src.BusinessEntityAddresses))
                .ForMember(m => m.ContactInfo, opt => opt.MapFrom((src, dest) =>
                {
                    var list = new List<ContactInfoDto>();

                    if (src.EmailAddresses.Count > 0)
                        list.AddRange(src.EmailAddresses.Select(e => new ContactInfoDto
                        {
                            ContactInfoChannelType = ContactInfoChannelTypeDto.Email,
                            Value = e.EmailAddress1
                        }));

                    if (src.PhoneNumbers.Count > 0)
                        list.AddRange(src.PhoneNumbers.Select(p => new ContactInfoDto
                        {
                            ContactInfoChannelType = ContactInfoChannelTypeDto.Phone,
                            ContactInfoType = p.PhoneNumberType.Name,
                            Value = p.PhoneNumber
                        }));

                    return list;
                }))
                .ReverseMap();
        }
    }
}