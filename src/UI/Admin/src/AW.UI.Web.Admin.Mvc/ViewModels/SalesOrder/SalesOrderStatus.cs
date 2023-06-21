using Ardalis.SmartEnum;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder;

public sealed class SalesOrderStatus : SmartEnum<SalesOrderStatus>
{
    [Display(Name = "In Process")]
    public static readonly SalesOrderStatus InProcess = new(nameof(InProcess), 1);
    public static readonly SalesOrderStatus Approved = new(nameof(Approved), 2);
    public static readonly SalesOrderStatus Backordered = new(nameof(Backordered), 3);
    public static readonly SalesOrderStatus Rejected = new(nameof(Rejected), 4);
    public static readonly SalesOrderStatus Shipped = new(nameof(Shipped), 5);
    public static readonly SalesOrderStatus Cancelled = new(nameof(Cancelled), 6);

    private SalesOrderStatus(string name, int value) : base(name, value)
    { 
    }
    public SalesOrderStatus() : base(nameof(InProcess), 1)
    {
    }

    public string GetBadgeCssClass()
    {
        return Name switch
        {
            nameof(InProcess) => "primary",
            nameof(Approved) => "success",
            nameof(Backordered) => "secondary",
            nameof(Rejected) => "danger",
            nameof(Shipped) => "info",
            nameof(Cancelled) => "warning",
            _ => "dark",
        };
    }

    public string GetDisplayName()
    {
        var displayAttribute = GetType()
            .GetMember(Name)
            .First()
            .GetCustomAttribute<DisplayAttribute>();

        if (displayAttribute == null)
            return Name;

        return displayAttribute!.Name!;
    }
}
