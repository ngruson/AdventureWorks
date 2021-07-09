using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.Interfaces;
using System.Collections.Generic;

namespace AW.Services.SalesPerson.Core.Handlers.GetSalesPersons
{
    public class SalesPersonDto : IMapFrom<Entities.SalesPerson>, IPerson
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Territory { get; set; }
        public List<SalesPersonEmailAddressDto> EmailAddresses { get; set; }
        public List<SalesPersonPhoneDto> PhoneNumbers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.SalesPerson, SalesPersonDto>();
        }
    }
}