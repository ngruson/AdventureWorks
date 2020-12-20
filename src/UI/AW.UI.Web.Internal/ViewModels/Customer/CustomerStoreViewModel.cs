using AutoMapper;
using AW.Core.Application.AutoMapper;
using GetCustomer = AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using ListCustomers = AW.Core.Abstractions.Api.CustomerApi.ListCustomers;
using UpdateCustomer = AW.Core.Abstractions.Api.CustomerApi.UpdateCustomer;
using System.Collections.Generic;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class CustomerStoreViewModel : IMapFrom<GetCustomer.Store>
    {
        public string Name { get; set; }
        
        public SalesPersonViewModel SalesPerson { get; set; }
        public List<CustomerAddressViewModel> Addresses { get; set; }
        public List<CustomerContactViewModel> Contacts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetCustomer.Store, CustomerStoreViewModel>();
            profile.CreateMap<ListCustomers.Store, CustomerStoreViewModel>();
            profile.CreateMap<CustomerStoreViewModel, UpdateCustomer.Store>();
        }
    }
}