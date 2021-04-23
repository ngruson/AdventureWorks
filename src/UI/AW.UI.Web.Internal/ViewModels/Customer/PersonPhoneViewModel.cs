using AutoMapper;
using AW.UI.Web.Internal.Common;
using m = AW.UI.Web.Internal.ApiClients.CustomerApi.Models;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class PersonPhoneViewModel : IMapFrom<m.GetCustomers.PersonPhone>
    {
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<m.GetCustomers.PersonPhone, PersonPhoneViewModel>();
            profile.CreateMap<m.GetCustomer.PersonPhone, PersonPhoneViewModel>();
            profile.CreateMap<PersonPhoneViewModel, m.UpdateCustomer.PersonPhone>();
        }
    }
}