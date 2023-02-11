using System.Collections.Generic;

public class UpdateOrderlinesSalesOrderViewModel
{
    public string? SalesOrderNumber { get; set; }
    public List<UpdateOrderlinesSalesOrderlineViewModel>? OrderLines { get; set; }
}