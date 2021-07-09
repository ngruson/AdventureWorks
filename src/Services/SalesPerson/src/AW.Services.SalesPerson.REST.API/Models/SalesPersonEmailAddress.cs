using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.SalesPerson.REST.API.Models
{
    public class SalesPersonEmailAddress : IMapFrom<Core.Handlers.GetSalesPersons.SalesPersonEmailAddressDto>
    {
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Handlers.GetSalesPersons.SalesPersonEmailAddressDto, SalesPersonEmailAddress>();
            profile.CreateMap<Core.Handlers.GetSalesPerson.SalesPersonEmailAddressDto, SalesPersonEmailAddress>();
        }
    }
}