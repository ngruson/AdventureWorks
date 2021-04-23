using AutoMapper;
using AW.UI.Web.Internal.ApiClients.CustomerApi.Models.GetCustomer;
using AW.UI.Web.Internal.Common;

namespace AW.UI.Web.Internal.ViewModels.Customer
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