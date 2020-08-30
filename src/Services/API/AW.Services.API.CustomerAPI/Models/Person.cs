using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customers;
using System.Collections.Generic;

namespace AW.Services.API.CustomerAPI.Models
{
    public class Person : IMapFrom<PersonDto>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Suffix { get; set; }
        public EmailPromotion EmailPromotion { get; set; }
        public List<CustomerAddress> Addresses { get; set; }
        public List<ContactInfo> ContactInfo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonDto, Person>();
        }
    }
}