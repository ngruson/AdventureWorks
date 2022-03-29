using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System.Collections.Generic;
using customerApi = AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class PersonViewModel : IMapFrom<customerApi.GetCustomers.Person>
    {
        public string Title { get; set; }
        public PersonNameViewModel Name { get; set; }        

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