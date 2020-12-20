using AutoMapper;
using AW.Core.Application.AutoMapper;
using GetCustomer = AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using ListCustomers = AW.Core.Abstractions.Api.CustomerApi.ListCustomers;
using AddCustomerContact = AW.Core.Abstractions.Api.CustomerApi.AddCustomerContact;
using UpdateCustomerContact = AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerContact;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class CustomerContactViewModel : IMapFrom<GetCustomer.CustomerContact>
    {
        [Display(Name = "Contact type")]
        [Required]
        public string ContactType { get; set; }
        public ContactViewModel Contact { get; set; } = new ContactViewModel();
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetCustomer.CustomerContact, CustomerContactViewModel>();
            profile.CreateMap<ListCustomers.CustomerContact, CustomerContactViewModel>();

            profile.CreateMap<CustomerContactViewModel, AddCustomerContact.CustomerContact>();
            profile.CreateMap<CustomerContactViewModel, UpdateCustomerContact.CustomerContact>();
        }
    }
}