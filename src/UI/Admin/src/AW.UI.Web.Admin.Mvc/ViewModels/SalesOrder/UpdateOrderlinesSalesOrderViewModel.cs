namespace AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder;

public class UpdateOrderlinesSalesOrderViewModel
{
    public string? SalesOrderNumber { get; set; }
    public List<UpdateOrderlinesSalesOrderlineViewModel>? OrderLines { get; set; }
}
