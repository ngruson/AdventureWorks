using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.Interfaces;
using System.ComponentModel.DataAnnotations;
using customerApi = AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class PersonNameViewModel : IPerson, IMapFrom<customerApi.GetCustomers.Person>
    {
        [Display(Name = "First name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Middle name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Full name")]
        [Required]
        public string FullName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<customerApi.GetCustomers.PersonName, PersonNameViewModel>();
            profile.CreateMap<customerApi.GetCustomer.PersonName, PersonNameViewModel>();
            profile.CreateMap<PersonNameViewModel, customerApi.UpdateCustomer.PersonName>();
        }
    }
}