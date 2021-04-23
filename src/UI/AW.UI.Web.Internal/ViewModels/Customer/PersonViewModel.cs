using AutoMapper;
using AW.UI.Web.Internal.Common;
using AW.UI.Web.Internal.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using m = AW.UI.Web.Internal.ApiClients.CustomerApi.Models;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class PersonViewModel : IPerson, IMapFrom<m.GetCustomers.Person>
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
            profile.CreateMap<m.GetCustomers.Person, PersonViewModel>();
            profile.CreateMap<m.GetCustomer.Person, PersonViewModel>();
            profile.CreateMap<PersonViewModel, m.UpdateCustomer.Person>();
        }
    }
}