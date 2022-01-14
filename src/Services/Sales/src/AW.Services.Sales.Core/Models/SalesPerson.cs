using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.Interfaces;
using System.Collections.Generic;

namespace AW.Services.Sales.Core.Models
{
    public class SalesPerson : IMapFrom<Handlers.GetSalesPerson.SalesPersonDto>, IPerson
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
            profile.CreateMap<Handlers.GetSalesPersons.SalesPersonDto, SalesPerson>();
            profile.CreateMap<Handlers.GetSalesPerson.SalesPersonDto, SalesPerson>();
        }
    }
}