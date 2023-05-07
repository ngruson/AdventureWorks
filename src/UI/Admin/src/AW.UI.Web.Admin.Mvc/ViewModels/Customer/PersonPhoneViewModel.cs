using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer
{
    public class PersonPhoneViewModel : IMapFrom<Infrastructure.Api.Customer.Handlers.GetCustomers.PersonPhone>
    {
        [Display(Name = "Phone number type")]
        [Required]
        public string? PhoneNumberType { get; set; }

        [Display(Name = "Phone number")]
        [Required]
        public string? PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetCustomers.PersonPhone, PersonPhoneViewModel>();
            profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetCustomer.PersonPhone, PersonPhoneViewModel>();
            profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetIndividualCustomer.PersonPhone, PersonPhoneViewModel>();
            profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetStoreCustomer.PersonPhone, PersonPhoneViewModel>();
            profile.CreateMap<PersonPhoneViewModel, Infrastructure.Api.Customer.Handlers.UpdateCustomer.PersonPhone>();
        }
    }
}
