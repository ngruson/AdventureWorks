using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.CustomerService;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class EditCustomerContactViewModel : IMapFrom<CustomerContact2>
    {
        public bool IsNewContact { get; set; }
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public CustomerContactViewModel CustomerContact { get; set; }
        public IEnumerable<SelectListItem> ContactTypes { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EditCustomerContactViewModel, AddCustomerContactRequest>();
            profile.CreateMap<EditCustomerContactViewModel, UpdateCustomerContactRequest>();
        }
    }
}