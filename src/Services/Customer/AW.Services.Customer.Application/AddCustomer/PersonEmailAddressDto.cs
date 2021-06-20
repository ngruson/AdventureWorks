﻿using AutoMapper;
using AW.Common.AutoMapper;

namespace AW.Services.Customer.Application.AddCustomer
{
    public class PersonEmailAddressDto : IMapFrom<Domain.PersonEmailAddress>
    {
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonEmailAddressDto, Domain.PersonEmailAddress>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}