using AutoMapper;
using AW.Services.SharedKernel.ValueTypes;
using AW.SharedKernel.AutoMapper;
using System.Collections.Generic;

namespace AW.Services.Sales.Core.Models
{
    public class SalesPerson : IMapFrom<Handlers.GetSalesPerson.SalesPersonDto>
    {        
        public string Title { get; set; }
        public NameFactory Name { get; set; }
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