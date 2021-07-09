using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.SalesPerson.Core.Handlers.GetSalesPersons
{
    public class SalesPersonPhoneDto : IMapFrom<Entities.SalesPersonPhone>
    {
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.SalesPersonPhone, SalesPersonPhoneDto>();
        }
    }
}