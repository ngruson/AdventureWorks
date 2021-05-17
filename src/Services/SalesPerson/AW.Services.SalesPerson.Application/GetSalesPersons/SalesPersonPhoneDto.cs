using AutoMapper;
using AW.Common.AutoMapper;

namespace AW.Services.SalesPerson.Application.GetSalesPersons
{
    public class SalesPersonPhoneDto : IMapFrom<Domain.SalesPersonPhone>
    {
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.SalesPersonPhone, SalesPersonPhoneDto>();
        }
    }
}