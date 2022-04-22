﻿using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;
using System.Collections.Generic;

namespace AW.Services.Customer.Core.Handlers.AddCustomer
{
    public class PersonDto : IMapFrom<Entities.Person>
    {
        public string Title { get; set; }
        public NameFactory Name { get; set; }
        public string Suffix { get; set; }
        public List<PersonEmailAddressDto> EmailAddresses { get; set; }
        public List<PersonPhoneDto> PhoneNumbers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonDto, Entities.Person>()
                .ForMember(m => m.EmailAddresses, opt =>
                    opt.MapFrom((src, dest, member, ctx) =>
                    {
                        src.EmailAddresses.ForEach(personEmailAddress =>
                            dest.AddEmailAddress(
                                ctx.Mapper.Map<Entities.PersonEmailAddress>(personEmailAddress)
                            )
                        );

                        return dest.EmailAddresses;
                    }
                    )
                )
                .ForMember(m => m.PhoneNumbers, opt =>
                    opt.MapFrom((src, dest, member, ctx) =>
                    {
                        src.PhoneNumbers.ForEach(personPhoneNumber =>
                            dest.AddPhoneNumber(
                                ctx.Mapper.Map<Entities.PersonPhone>(personPhoneNumber)
                            )
                        );

                        return dest.PhoneNumbers;
                    }
                    )
                )
                .ReverseMap();
        }
    }
}