using AutoMapper;
using AW.Core.Application.AutoMapper;
using GetCustomer = AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using ListCustomers = AW.Core.Abstractions.Api.CustomerApi.ListCustomers;
using UpdateCustomer = AW.Core.Abstractions.Api.CustomerApi.UpdateCustomer;
using System.Collections.Generic;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class CustomerPersonViewModel : IMapFrom<GetCustomer.Person>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Suffix { get; set; }
        public EmailPromotionViewModel EmailPromotion { get; set; }
        public List<CustomerAddressViewModel> Addresses { get; set; }
        public List<ContactInfoViewModel> ContactInfo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetCustomer.Person, CustomerPersonViewModel>();
            profile.CreateMap<ListCustomers.Person, CustomerPersonViewModel>();
            profile.CreateMap<CustomerPersonViewModel, UpdateCustomer.Person>();
        }
    }
}