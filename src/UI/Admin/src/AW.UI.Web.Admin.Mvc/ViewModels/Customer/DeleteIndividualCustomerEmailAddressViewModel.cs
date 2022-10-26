using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.UI.Web.SharedKernel.Customer.Handlers.GetIndividualCustomer;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer
{
    public class DeleteIndividualCustomerEmailAddressViewModel : IMapFrom<PersonEmailAddress>
    {
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonEmailAddress, DeleteIndividualCustomerEmailAddressViewModel>()
                .ForMember(m => m.AccountNumber, opt => opt.Ignore())
                .ForMember(m => m.CustomerName, opt => opt.Ignore());
        }
    }
}