using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.UI.Web.SharedKernel.Customer.Handlers.GetCustomer;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer
{
    public class CustomerContactViewModel : IMapFrom<StoreCustomerContact>
    {
        [Display(Name = "Contact type")]
        [Required]
        public string? ContactType { get; set; }
        public PersonViewModel ContactPerson { get; set; } = new PersonViewModel();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.Customer.Handlers.GetCustomers.StoreCustomerContact, CustomerContactViewModel>();
            profile.CreateMap<StoreCustomerContact, CustomerContactViewModel>();
            profile.CreateMap<SharedKernel.Customer.Handlers.GetStoreCustomer.StoreCustomerContact, CustomerContactViewModel>();
            profile.CreateMap<CustomerContactViewModel, SharedKernel.Customer.Handlers.UpdateCustomer.StoreCustomerContact>();
        }
    }
}