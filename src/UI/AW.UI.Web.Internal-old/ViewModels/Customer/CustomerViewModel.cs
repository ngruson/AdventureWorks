using AW.UI.Web.Internal.ApiClients.CustomerApi.Models.GetCustomers;
using AW.UI.Web.Internal.ViewModels.SalesOrder;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public abstract class CustomerViewModel
    {
        public abstract CustomerType CustomerType { get; }

        [Display(Name="Account number")]
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }

        [Display(Name = "Sales territory")]
        public string Territory { get; set; }

        public List<CustomerAddressViewModel> Addresses { get; set; }
        public List<SalesOrderViewModel> SalesOrders { get; set; }
    }
}