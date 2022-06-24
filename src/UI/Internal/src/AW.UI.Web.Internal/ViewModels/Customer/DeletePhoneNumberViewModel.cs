using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models.GetCustomer;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class DeletePhoneNumberViewModel : IMapFrom<PersonPhone>
    {
        public string AccountNumber { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonPhone, DeletePhoneNumberViewModel>()
                .ForMember(m => m.AccountNumber, opt => opt.Ignore())
                .ForMember(m => m.ContactName, opt => opt.Ignore());
        }
    }
}