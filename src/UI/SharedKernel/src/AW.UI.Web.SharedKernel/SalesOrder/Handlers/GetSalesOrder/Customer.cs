using AW.SharedKernel.Interfaces;
using System.Text.Json.Serialization;

namespace AW.UI.Web.SharedKernel.SalesOrder.Handlers.GetSalesOrder
{
    public abstract class Customer : ICustomer
    {
        public AW.SharedKernel.Interfaces.CustomerType CustomerType { get; set; }
        public string? CustomerNumber { get; set; }

        public virtual string? CustomerName { get; }

        public int SalesOrderCount { get; set; }
    }
}
