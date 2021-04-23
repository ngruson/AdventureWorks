using AutoMapper;
using AW.Services.SalesPerson.Application.Common;
using System.Collections.Generic;

namespace AW.Services.SalesPerson.REST.API.Models
{
    public class SalesPerson : IMapFrom<Application.GetSalesPersons.SalesPersonDto>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Territory { get; set; }
        public List<SalesPersonEmailAddress> EmailAddresses { get; set; }
        public List<SalesPersonPhone> PhoneNumbers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Application.GetSalesPersons.SalesPersonDto, SalesPerson>();
            profile.CreateMap<Application.GetSalesPerson.SalesPersonDto, SalesPerson>();
        }
    }
}