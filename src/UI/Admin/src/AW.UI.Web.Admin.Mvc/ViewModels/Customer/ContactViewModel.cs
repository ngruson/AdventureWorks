using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer
{
    public class ContactViewModel : IMapFrom<Infrastructure.Api.Customer.Handlers.GetCustomers.StoreCustomerContact>
    {
        public string? ContactType { get; set; }
        public PersonViewModel? ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetCustomers.StoreCustomerContact, ContactViewModel>();
            profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetCustomer.StoreCustomerContact, ContactViewModel>();
            profile.CreateMap<ContactViewModel, Infrastructure.Api.Customer.Handlers.UpdateCustomer.StoreCustomerContact>();
        }
    }
}
