using AutoMapper;
using AW.Common.AutoMapper;

namespace AW.Services.SalesPerson.REST.API.Models
{
    public class SalesPersonEmailAddress : IMapFrom<Application.GetSalesPersons.SalesPersonEmailAddressDto>
    {
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Application.GetSalesPersons.SalesPersonEmailAddressDto, SalesPersonEmailAddress>();
            profile.CreateMap<Application.GetSalesPerson.SalesPersonEmailAddressDto, SalesPersonEmailAddress>();
        }
    }
}