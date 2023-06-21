using AW.SharedKernel.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer;

public abstract class CustomerViewModel
{
    public Guid ObjectId { get; set; }
    public abstract CustomerType CustomerType { get; }

    [Display(Name = "Account number")]
    public string? AccountNumber { get; set; }
    public string? CustomerName { get; set; }

    [Display(Name = "Sales territory")]
    public string? Territory { get; set; }

    public List<CustomerAddressViewModel> Addresses { get; set; } = new();
    public List<SalesOrderViewModel> SalesOrders { get; set; } = new();
}
