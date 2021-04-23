using AutoMapper;
using AW.Services.SalesPerson.Application.Common;

namespace AW.Services.SalesPerson.Application.GetSalesPerson
{
    public class SalesPersonEmailAddressDto : IMapFrom<Domain.SalesPersonEmailAddress>
    {
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.SalesPersonEmailAddress, SalesPersonEmailAddressDto>();
        }
    }
}