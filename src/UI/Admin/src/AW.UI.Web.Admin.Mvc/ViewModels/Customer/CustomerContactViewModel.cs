using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer
{
    public class CustomerContactViewModel : IMapFrom<Infrastructure.Api.Customer.Handlers.GetCustomers.StoreCustomerContact>
    {
        [Display(Name = "Contact type")]
        [Required]
        public string? ContactType { get; set; }
        public PersonViewModel ContactPerson { get; set; } = new PersonViewModel();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetCustomers.StoreCustomerContact, CustomerContactViewModel>();
            profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetCustomer.StoreCustomerContact, CustomerContactViewModel>();
            profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetStoreCustomer.StoreCustomerContact, CustomerContactViewModel>();
            profile.CreateMap<CustomerContactViewModel, Infrastructure.Api.Customer.Handlers.UpdateCustomer.StoreCustomerContact>();
        }
    }
}
