using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.UI.Web.SharedKernel.Customer.Handlers.GetCustomer;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer
{
    public class PersonEmailAddressViewModel : IMapFrom<SharedKernel.Customer.Handlers.GetCustomers.PersonEmailAddress>
    {
        [Display(Name = "Email address")]
        [Required]
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.Customer.Handlers.GetCustomers.PersonEmailAddress, PersonEmailAddressViewModel>();
            profile.CreateMap<PersonEmailAddress, PersonEmailAddressViewModel>();
            profile.CreateMap<SharedKernel.Customer.Handlers.GetIndividualCustomer.PersonEmailAddress, PersonEmailAddressViewModel>();
            profile.CreateMap<SharedKernel.Customer.Handlers.GetStoreCustomer.PersonEmailAddress, PersonEmailAddressViewModel>();
            profile.CreateMap<PersonEmailAddressViewModel, SharedKernel.Customer.Handlers.UpdateCustomer.PersonEmailAddress>();
        }
    }
}