using AutoMapper;
using AW.SharedKernel.AutoMapper;
using m = AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class ContactViewModel : IMapFrom<m.GetCustomers.StoreCustomerContact>
    {
        public string ContactType { get; set; }
        public PersonViewModel ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<m.GetCustomers.StoreCustomerContact, ContactViewModel>();
            profile.CreateMap<m.GetCustomer.StoreCustomerContact, ContactViewModel>();
            profile.CreateMap<ContactViewModel, m.UpdateCustomer.StoreCustomerContact>();
        }
    }
}