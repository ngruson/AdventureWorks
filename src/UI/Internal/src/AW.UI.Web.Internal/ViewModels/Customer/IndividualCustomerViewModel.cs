using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.Interfaces;
using m = AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class IndividualCustomerViewModel : CustomerViewModel, IMapFrom<m.GetCustomers.IndividualCustomer>
    {
        public override CustomerType CustomerType => CustomerType.Individual;

        public PersonViewModel Person { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<m.GetCustomers.IndividualCustomer, IndividualCustomerViewModel>();
            profile.CreateMap<m.GetCustomer.IndividualCustomer, IndividualCustomerViewModel>()
                .ForMember(m => m.SalesOrders, opt => opt.Ignore());
            profile.CreateMap<IndividualCustomerViewModel, m.UpdateCustomer.IndividualCustomer>();
        }
    }
}