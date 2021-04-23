using AutoMapper;
using AW.Services.SalesPerson.Application.Common;

namespace AW.Services.SalesPerson.REST.API.Models
{
    public class SalesPersonPhone : IMapFrom<Application.GetSalesPersons.SalesPersonPhoneDto>
    {
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Application.GetSalesPersons.SalesPersonPhoneDto, SalesPersonPhone>();
            profile.CreateMap<Application.GetSalesPerson.SalesPersonPhoneDto, SalesPersonPhone>();
        }
    }
}