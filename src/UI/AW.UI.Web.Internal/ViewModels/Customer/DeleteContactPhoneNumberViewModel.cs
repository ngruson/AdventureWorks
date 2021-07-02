using AutoMapper;
using AW.Common.AutoMapper;
using AW.UI.Web.Common.ApiClients.CustomerApi.Models.GetCustomer;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class DeleteContactPhoneNumberViewModel : IMapFrom<PersonPhone>
    {
        public string AccountNumber { get; set; }
        public string ContactType { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonPhone, DeleteContactPhoneNumberViewModel>()
                .ForMember(m => m.AccountNumber, opt => opt.Ignore())
                .ForMember(m => m.ContactType, opt => opt.Ignore())
                .ForMember(m => m.ContactName, opt => opt.Ignore());
        }
    }
}