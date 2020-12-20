using AutoMapper;
using AW.Core.Application.AutoMapper;
using ListCustomers = AW.Core.Abstractions.Api.CustomerApi.ListCustomers;
using GetCustomer = AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using AddCustomerContact = AW.Core.Abstractions.Api.CustomerApi.AddCustomerContact;
using UpdateCustomerContact = AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerContact;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class ContactViewModel : IMapFrom<GetCustomer.Contact>
    {
        public string Title { get; set; }

        [Display(Name = "First name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Middle name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last name")]
        [Required]
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Suffix { get; set; }        

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ListCustomers.Contact, ContactViewModel>();
            profile.CreateMap<GetCustomer.Contact, ContactViewModel>();

            profile.CreateMap<ContactViewModel, AddCustomerContact.Contact>()
                .ForMember(m => m.EmailAddresses, opt => opt.Ignore());
            profile.CreateMap<ContactViewModel, UpdateCustomerContact.Contact>()
                .ForMember(m => m.EmailAddresses, opt => opt.Ignore());
        }
    }
}