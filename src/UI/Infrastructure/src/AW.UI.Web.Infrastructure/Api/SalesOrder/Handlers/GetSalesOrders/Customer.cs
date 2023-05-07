using AW.SharedKernel.Interfaces;
using System.Text.Json.Serialization;

namespace AW.UI.Web.Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders
{
    public abstract class Customer : ICustomer
    {
        public AW.SharedKernel.Interfaces.CustomerType CustomerType { get; set; }
        public string? CustomerNumber { get; set; }

        public string? CustomerName { get; set; }

        public int SalesOrderCount { get; set; }
    }
}
