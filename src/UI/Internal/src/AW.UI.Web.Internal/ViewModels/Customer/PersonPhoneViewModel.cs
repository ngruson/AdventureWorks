using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.UI.Web.SharedKernel.Customer.Handlers.GetCustomer;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class PersonPhoneViewModel : IMapFrom<SharedKernel.Customer.Handlers.GetCustomers.PersonPhone>
    {
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }

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