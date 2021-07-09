using AutoMapper;
using AW.Services.SalesPerson.Core.Handlers.GetSalesPerson;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.SalesPerson.WCF.Messages.GetSalesPerson
{
    public class SalesPersonPhone : IMapFrom<SalesPersonPhoneDto>
    {
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesPersonPhoneDto, SalesPersonPhone>();
        }
    }
}