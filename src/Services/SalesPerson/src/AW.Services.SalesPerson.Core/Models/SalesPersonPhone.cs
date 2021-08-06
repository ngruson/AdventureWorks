using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.SalesPerson.Core.Models
{
    public class SalesPersonPhone : IMapFrom<Handlers.GetSalesPersons.SalesPersonPhoneDto>
    {
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Handlers.GetSalesPersons.SalesPersonPhoneDto, SalesPersonPhone>();
            profile.CreateMap<Handlers.GetSalesPerson.SalesPersonPhoneDto, SalesPersonPhone>();
        }
    }
}