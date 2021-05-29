﻿using AutoMapper;
using AW.Common.AutoMapper;

namespace AW.Services.Customer.Application.AddStoreCustomerContact
{
    public class StoreCustomerContactDto : IMapFrom<Domain.StoreCustomerContact>
    {
        public string ContactType { get; set; }
        public PersonDto ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreCustomerContactDto, Domain.StoreCustomerContact>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.StoreCustomer, opt => opt.Ignore())
                .ForMember(m => m.StoreCustomerId, opt => opt.Ignore())
                .ForMember(m => m.ContactPersonId, opt => opt.Ignore());
        }
    }
}