using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.UI.Web.SharedKernel.Customer.Handlers.GetCustomer;
using System.Collections.Generic;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer
{
    public class PersonViewModel : IMapFrom<SharedKernel.Customer.Handlers.GetCustomers.Person>
    {
        public string Title { get; set; }
        public PersonNameViewModel Name { get; set; }

        public string Suffix { get; set; }

        public List<PersonEmailAddressViewModel> EmailAddresses { get; set; } = new();
        public List<PersonPhoneViewModel> PhoneNumbers { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.Customer.Handlers.GetCustomers.Person, PersonViewModel>();
            profile.CreateMap<Person, PersonViewModel>();
            profile.CreateMap<SharedKernel.Customer.Handlers.GetIndividualCustomer.Person, PersonViewModel>();
            profile.CreateMap<SharedKernel.Customer.Handlers.GetStoreCustomer.Person, PersonViewModel>();
            profile.CreateMap<PersonViewModel, SharedKernel.Customer.Handlers.UpdateCustomer.Person>();
        }
    }
}