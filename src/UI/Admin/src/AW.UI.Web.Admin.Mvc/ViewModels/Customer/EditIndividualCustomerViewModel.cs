using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer
{
    public class EditIndividualCustomerViewModel
    {
        public IndividualCustomerViewModel Customer { get; set; }
        public IEnumerable<SelectListItem> Territories { get; set; }
        public IEnumerable<SelectListItem> EmailPromotions { get; set; }
    }
}