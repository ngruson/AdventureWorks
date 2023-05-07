using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.Interfaces;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer
{
    public class IndividualCustomerViewModel : CustomerViewModel, IMapFrom<Infrastructure.Api.Customer.Handlers.GetCustomers.IndividualCustomer>
    {
        public override CustomerType CustomerType => CustomerType.Individual;

        public PersonViewModel? Person { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetCustomers.IndividualCustomer, IndividualCustomerViewModel>();
            profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetCustomer.IndividualCustomer, IndividualCustomerViewModel>();
            profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetIndividualCustomer.IndividualCustomer, IndividualCustomerViewModel>();
            profile.CreateMap<IndividualCustomerViewModel, Infrastructure.Api.Customer.Handlers.UpdateCustomer.IndividualCustomer>();
        }
    }
}
