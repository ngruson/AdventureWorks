using AutoMapper;
using AW.Common.AutoMapper;
using m = AW.UI.Web.Common.ApiClients.CustomerApi.Models;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class PersonEmailAddressViewModel : IMapFrom<m.GetCustomers.PersonEmailAddress>
    {
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<m.GetCustomers.PersonEmailAddress, PersonEmailAddressViewModel>();
            profile.CreateMap<m.GetCustomer.PersonEmailAddress, PersonEmailAddressViewModel>();
            profile.CreateMap<PersonEmailAddressViewModel, m.UpdateCustomer.PersonEmailAddress>();
        }
    }
}