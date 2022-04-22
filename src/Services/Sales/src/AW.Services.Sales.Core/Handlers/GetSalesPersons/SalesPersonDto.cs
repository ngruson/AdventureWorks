using AutoMapper;
using AW.Services.SharedKernel.ValueTypes;
using AW.SharedKernel.AutoMapper;
using System.Collections.Generic;

namespace AW.Services.Sales.Core.Handlers.GetSalesPersons
{
    public class SalesPersonDto : IMapFrom<Entities.SalesPerson>
    {
        public string Title { get; set; }
        public NameFactory Name { get; private set; }
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