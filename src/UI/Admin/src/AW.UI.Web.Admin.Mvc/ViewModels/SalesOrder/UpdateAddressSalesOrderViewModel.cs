namespace AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder
{
    public class UpdateAddressSalesOrderViewModel
    {
        public string SalesOrderNumber { get; set; }
        public AddressViewModel ShipToAddress { get; set; }
        public AddressViewModel BillToAddress { get; set; }
    }
}