using AutoMapper;
using AW.Core.Application.AutoMapper;
using AddCustomerContact = AW.Core.Abstractions.Api.CustomerApi.AddCustomerContact;
using UpdateCustomerContact = AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerContact;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class EditCustomerContactViewModel : IMapFrom<AddCustomerContact.CustomerContact>
    {
        public bool IsNewContact { get; set; }
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public CustomerContactViewModel CustomerContact { get; set; }
        public IEnumerable<SelectListItem> ContactTypes { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EditCustomerContactViewModel, AddCustomerContact.AddCustomerContactRequest>();
            profile.CreateMap<EditCustomerContactViewModel, UpdateCustomerContact.UpdateCustomerContactRequest>();
        }
    }
}