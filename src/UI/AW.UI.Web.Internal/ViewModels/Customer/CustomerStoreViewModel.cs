using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.CustomerService;
using System.Collections.Generic;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class CustomerStoreViewModel : IMapFrom<Store>
    {
        public string Name { get; set; }
        
        public SalesPersonViewModel SalesPerson { get; set; }
        public List<CustomerAddressViewModel> Addresses { get; set; }
        public List<CustomerContactViewModel> Contacts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Store, CustomerStoreViewModel>();
            profile.CreateMap<Store1, CustomerStoreViewModel>();
            profile.CreateMap<CustomerStoreViewModel, UpdateStore>();
        }
    }
}