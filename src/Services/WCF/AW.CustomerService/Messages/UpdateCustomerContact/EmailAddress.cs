﻿using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer.UpdateCustomerContact;
using System.Xml.Serialization;

namespace AW.CustomerService.Messages.UpdateCustomerContact
{
    public class EmailAddress : IMapFrom<EmailAddressDto>
    {
        [XmlElement(ElementName = "EmailAddress")]
        public string EmailAddress1 { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmailAddressDto, EmailAddress>()
                .ForMember(m => m.EmailAddress1, opt => opt.MapFrom(src => src.EmailAddress));
        }
    }
}