using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.UI.Web.SharedKernel.Customer.Handlers.GetIndividualCustomer;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer
{
    public class ContactViewModel : IMapFrom<SharedKernel.Customer.Handlers.GetCustomers.StoreCustomerContact>
    {
        public string ContactType { get; set; }
        public PersonViewModel ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.Customer.Handlers.GetCustomers.StoreCustomerContact, ContactViewModel>();
            profile.CreateMap<SharedKernel.Customer.Handlers.GetCustomer.StoreCustomerContact, ContactViewModel>();
            profile.CreateMap<ContactViewModel, SharedKernel.Customer.Handlers.UpdateCustomer.StoreCustomerContact>();
        }
    }
}