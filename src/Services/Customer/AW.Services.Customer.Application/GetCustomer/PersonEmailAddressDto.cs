﻿using AutoMapper;
using AW.Common.AutoMapper;

namespace AW.Services.Customer.Application.GetCustomer
{
    public class PersonEmailAddressDto : IMapFrom<Domain.PersonEmailAddress>
    {
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.PersonEmailAddress, PersonEmailAddressDto>();
        }
    }
}