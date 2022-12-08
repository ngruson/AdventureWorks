using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.Interfaces;
using AW.UI.Web.SharedKernel.Customer.Handlers.GetCustomer;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer
{
    public class IndividualCustomerViewModel : CustomerViewModel, IMapFrom<SharedKernel.Customer.Handlers.GetCustomers.IndividualCustomer>
    {
        public override CustomerType CustomerType => CustomerType.Individual;

        public PersonViewModel Person { get; set; }

        public IEnumerable<SelectListItem> PhoneNumberTypes { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.Customer.Handlers.GetCustomers.IndividualCustomer, IndividualCustomerViewModel>()
                .ForMember(_ => _.PhoneNumberTypes, opt => opt.Ignore());
            profile.CreateMap<IndividualCustomer, IndividualCustomerViewModel>()
                .ForMember(_ => _.PhoneNumberTypes, opt => opt.Ignore());
            profile.CreateMap<SharedKernel.Customer.Handlers.GetIndividualCustomer.IndividualCustomer, IndividualCustomerViewModel>()
                .ForMember(_ => _.PhoneNumberTypes, opt => opt.Ignore());
            profile.CreateMap<IndividualCustomerViewModel, SharedKernel.Customer.Handlers.UpdateCustomer.IndividualCustomer>();
        }
    }
}