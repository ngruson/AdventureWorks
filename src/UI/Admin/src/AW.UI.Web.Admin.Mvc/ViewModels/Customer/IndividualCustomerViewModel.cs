using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.Interfaces;
using AW.UI.Web.SharedKernel.Customer.Handlers.GetCustomer;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer
{
    public class IndividualCustomerViewModel : CustomerViewModel, IMapFrom<SharedKernel.Customer.Handlers.GetCustomers.IndividualCustomer>
    {
        public override CustomerType CustomerType => CustomerType.Individual;

        public PersonViewModel? Person { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.Customer.Handlers.GetCustomers.IndividualCustomer, IndividualCustomerViewModel>();
            profile.CreateMap<IndividualCustomer, IndividualCustomerViewModel>();
            profile.CreateMap<SharedKernel.Customer.Handlers.GetIndividualCustomer.IndividualCustomer, IndividualCustomerViewModel>();
            profile.CreateMap<IndividualCustomerViewModel, SharedKernel.Customer.Handlers.UpdateCustomer.IndividualCustomer>();
        }
    }
}
