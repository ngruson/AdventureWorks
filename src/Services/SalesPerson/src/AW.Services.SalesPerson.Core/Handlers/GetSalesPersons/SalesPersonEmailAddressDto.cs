﻿using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.SalesPerson.Core.Handlers.GetSalesPersons
{
    public class SalesPersonEmailAddressDto : IMapFrom<Entities.SalesPersonEmailAddress>
    {
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.SalesPersonEmailAddress, SalesPersonEmailAddressDto>();
        }
    }
}