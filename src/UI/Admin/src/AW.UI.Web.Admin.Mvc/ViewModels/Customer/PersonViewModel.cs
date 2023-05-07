using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer
{
    public class PersonViewModel : IMapFrom<Infrastructure.Api.Customer.Handlers.GetCustomers.Person>
    {
        public string? Title { get; set; }
        public EmployeeNameViewModel? Name { get; set; }

        public string? Suffix { get; set; }

        public List<PersonEmailAddressViewModel> EmailAddresses { get; set; } = new();
        public List<PersonPhoneViewModel> PhoneNumbers { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetCustomers.Person, PersonViewModel>();
            profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetCustomer.Person, PersonViewModel>();
            profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetIndividualCustomer.Person, PersonViewModel>();
            profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetStoreCustomer.Person, PersonViewModel>();
            profile.CreateMap<PersonViewModel, Infrastructure.Api.Customer.Handlers.UpdateCustomer.Person>();
        }
    }
}
