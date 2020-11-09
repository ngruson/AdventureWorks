using AutoMapper;
using AW.Application.AutoMapper;
using AW.Domain.Person;
using System;
using System.Collections.Generic;

namespace AW.Application.Customer.UpdateCustomerContact
{
    public class ContactDto : IMapFrom<Person>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public List<EmailAddressDto> EmailAddresses { get; set; } = new List<EmailAddressDto>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ContactDto, Person>()
                .ForMember(m => m.PersonType, opt => opt.MapFrom(src => "SC"))
                .ForMember(m => m.NameStyle, opt => opt.MapFrom(src => 0))
                .ForMember(m => m.EmailPromotion, opt => opt.MapFrom(src => EmailPromotion.NoPromotions))
                .ForMember(m => m.rowguid, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(m => m.ModifiedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ReverseMap();
        }
    }
}