using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using customerApi = AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class PersonViewModel : IPerson, IMapFrom<customerApi.GetCustomers.Person>
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

        public string Suffix { get; set; }

        public List<PersonEmailAddressViewModel> EmailAddresses { get; set; }
        public List<PersonPhoneViewModel> PhoneNumbers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<customerApi.GetCustomers.Person, PersonViewModel>();
            profile.CreateMap<customerApi.GetCustomer.Person, PersonViewModel>();
            profile.CreateMap<PersonViewModel, customerApi.UpdateCustomer.Person>();
        }
    }
}