using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer
{
    public class PersonEmailAddressViewModel : IMapFrom<Infrastructure.Api.Customer.Handlers.GetCustomers.PersonEmailAddress>
    {
        [Display(Name = "Email address")]
        [Required]
        public string? EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetCustomers.PersonEmailAddress, PersonEmailAddressViewModel>();
            profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetCustomer.PersonEmailAddress, PersonEmailAddressViewModel>();
            profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetIndividualCustomer.PersonEmailAddress, PersonEmailAddressViewModel>();
            profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetStoreCustomer.PersonEmailAddress, PersonEmailAddressViewModel>();
            profile.CreateMap<PersonEmailAddressViewModel, Infrastructure.Api.Customer.Handlers.UpdateCustomer.PersonEmailAddress>();
        }
    }
}
