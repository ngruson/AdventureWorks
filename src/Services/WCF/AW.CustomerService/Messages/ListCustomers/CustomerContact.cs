﻿using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer.GetCustomers;
using System.Xml.Serialization;

namespace AW.CustomerService.Messages.ListCustomers
{
    public class CustomerContact : IMapFrom<CustomerContactDto>
    {
        [XmlElement(ElementName = "ContactType")]
        public string ContactTypeName { get; set; }
        public Contact Contact { get; set; }
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerContactDto, CustomerContact>();
        }
    }
}