using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.UI.Web.SharedKernel.Customer.Handlers.GetCustomer;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer
{
    public class PersonPhoneViewModel : IMapFrom<SharedKernel.Customer.Handlers.GetCustomers.PersonPhone>
    {
        [Display(Name = "Phone number type")]
        [Required]
        public string? PhoneNumberType { get; set; }

        [Display(Name = "Phone number")]
        [Required]
        public string? PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.Customer.Handlers.GetCustomers.PersonPhone, PersonPhoneViewModel>();
            profile.CreateMap<PersonPhone, PersonPhoneViewModel>();
            profile.CreateMap<SharedKernel.Customer.Handlers.GetIndividualCustomer.PersonPhone, PersonPhoneViewModel>();
            profile.CreateMap<SharedKernel.Customer.Handlers.GetStoreCustomer.PersonPhone, PersonPhoneViewModel>();
            profile.CreateMap<PersonPhoneViewModel, SharedKernel.Customer.Handlers.UpdateCustomer.PersonPhone>();
        }
    }
}