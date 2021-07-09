using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.SalesPerson.REST.API.Models
{
    public class SalesPersonPhone : IMapFrom<Core.Handlers.GetSalesPersons.SalesPersonPhoneDto>
    {
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Handlers.GetSalesPersons.SalesPersonPhoneDto, SalesPersonPhone>();
            profile.CreateMap<Core.Handlers.GetSalesPerson.SalesPersonPhoneDto, SalesPersonPhone>();
        }
    }
}