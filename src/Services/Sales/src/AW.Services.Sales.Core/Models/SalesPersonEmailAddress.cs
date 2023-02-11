using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Models
{
    public class SalesPersonEmailAddress : IMapFrom<Handlers.GetSalesPerson.SalesPersonEmailAddressDto>
    {
        public string? EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Handlers.GetSalesPersons.SalesPersonEmailAddressDto, SalesPersonEmailAddress>();
            profile.CreateMap<Handlers.GetSalesPerson.SalesPersonEmailAddressDto, SalesPersonEmailAddress>();
        }
    }
}