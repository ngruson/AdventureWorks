using AW.SharedKernel.Interfaces;
using System.Text.Json.Serialization;

namespace AW.UI.Web.SharedKernel.SalesOrder.Handlers.UpdateSalesOrder
{
    public abstract class Customer
    {
        public CustomerType CustomerType { get; set; }
        public string? CustomerNumber { get; set; }

        [JsonIgnore]
        public virtual string? CustomerName { get; }
    }
}