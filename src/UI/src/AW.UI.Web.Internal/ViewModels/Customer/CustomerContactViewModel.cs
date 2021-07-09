using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System.ComponentModel.DataAnnotations;
using m = AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class CustomerContactViewModel : IMapFrom<m.GetCustomer.StoreCustomerContact>
    {
        [Display(Name = "Contact type")]
        [Required]
        public string ContactType { get; set; }
        public PersonViewModel ContactPerson { get; set; } = new PersonViewModel();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<m.GetCustomer.StoreCustomerContact, CustomerContactViewModel>();
            profile.CreateMap<m.GetCustomers.StoreCustomerContact, CustomerContactViewModel>();
            profile.CreateMap<CustomerContactViewModel, m.UpdateCustomer.StoreCustomerContact>();
        }
    }
}