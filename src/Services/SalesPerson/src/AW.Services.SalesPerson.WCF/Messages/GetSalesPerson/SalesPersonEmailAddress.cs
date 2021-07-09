using AutoMapper;
using AW.Services.SalesPerson.Core.Handlers.GetSalesPerson;
using AW.SharedKernel.AutoMapper;

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