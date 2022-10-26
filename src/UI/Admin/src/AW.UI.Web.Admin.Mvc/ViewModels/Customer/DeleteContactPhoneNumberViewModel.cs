using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.UI.Web.SharedKernel.Customer.Handlers.GetIndividualCustomer;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer
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