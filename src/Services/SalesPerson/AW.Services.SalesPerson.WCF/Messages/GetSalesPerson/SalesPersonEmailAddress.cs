using AutoMapper;
using AW.Common.AutoMapper;
using AW.Services.SalesPerson.Application.GetSalesPerson;

namespace AW.Services.SalesPerson.WCF.Messages.GetSalesPerson
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