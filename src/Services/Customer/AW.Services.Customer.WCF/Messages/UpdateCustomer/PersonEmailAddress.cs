﻿using AutoMapper;
using AW.Common.AutoMapper;
using AW.Services.Customer.Application.UpdateCustomer;

namespace AW.Services.Customer.WCF.Messages.UpdateCustomer
{
    public class PersonEmailAddress : IMapFrom<PersonEmailAddressDto>
    {
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonEmailAddress, PersonEmailAddressDto>()
                .ReverseMap();
        }
    }
}