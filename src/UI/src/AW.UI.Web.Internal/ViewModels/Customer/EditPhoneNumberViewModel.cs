using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class EditPhoneNumberViewModel
    {
        public bool IsNewPhoneNumber{ get; set; }
        public string AccountNumber { get; set; }
        public string PersonName { get; set; }
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }
        public List<SelectListItem> PhoneNumberTypes { get; set; }
    }
}