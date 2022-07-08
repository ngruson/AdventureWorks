using System.Text.Json.Serialization;

namespace AW.UI.Web.SharedKernel.Customer.Handlers.GetStoreCustomer
{
    public class SalesOrder
    {
        public DateTime OrderDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ShipDate { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SalesOrderStatus Status { get; set; }

        public bool OnlineOrderFlag { get; set; }

        public string? SalesOrderNumber { get; set; }

        public string? PurchaseOrderNumber { get; set; }

        public string? AccountNumber { get; set; }

        public decimal TotalDue { get; set; }
    }
}