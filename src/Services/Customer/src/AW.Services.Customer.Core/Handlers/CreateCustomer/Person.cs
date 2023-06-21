using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.Services.Customer.Core.Handlers.CreateCustomer
{
    public class Person : IMapFrom<Entities.Person>
    {
        public Person() { }
        //public Person(string title, NameFactory name, string suffix,
        //       List<PersonEmailAddress>? emailAddresses,
        //       List<PersonPhone>? phoneNumbers
        //)
        //{
        //    Title = title;
        //    Name = name;
        //    Suffix = suffix;
        //    EmailAddresses = emailAddresses;
        //    PhoneNumbers = phoneNumbers;
        //}

        public Guid ObjectId { get; set; }
        public string? Title { get; set; }
        public NameFactory? Name { get; set; }
        public string? Suffix { get; set; }
        public List<PersonEmailAddress>? EmailAddresses { get; set; }
        public List<PersonPhone>? PhoneNumbers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Person, Entities.Person>()
                .ForMember(_ => _.Id, opt => opt.Ignore())
                .ForMember(_ => _.EmailAddresses, opt =>
                    opt.MapFrom((src, dest, member, ctx) =>
                    {
                        src.EmailAddresses!.ForEach(personEmailAddress =>
                            dest.AddEmailAddress(
                                ctx.Mapper.Map<Entities.PersonEmailAddress>(personEmailAddress)
                            )
                        );

                        return dest.EmailAddresses;
                    }
                    )
                )
                .ForMember(_ => _.PhoneNumbers, opt =>
                    opt.MapFrom((src, dest, member, ctx) =>
                    {
                        src.PhoneNumbers!.ForEach(personPhoneNumber =>
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
