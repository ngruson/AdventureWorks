using AutoMapper;
using AW.Services.SalesPerson.Application.Common;
using AW.Services.SalesPerson.Application.GetSalesPersons;

namespace AW.Services.SalesPerson.WCF.Messages.ListSalesPersons
{
    public class SalesPersonEmailAddress : IMapFrom<SalesPersonEmailAddressDto>
    {
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesPersonEmailAddressDto, SalesPersonEmailAddress>();
        }
    }
}