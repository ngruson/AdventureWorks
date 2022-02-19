using AW.Services.Customer.Core.Handlers.GetCustomer;
using AW.Services.Customer.Core.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AW.Services.Customer.Core.Models.GetCustomer
{
    public abstract class Customer : ICustomer
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Interfaces.CustomerType CustomerType { get; set; }

        public string AccountNumber { get; set; }
        public string Territory { get; set; }
        public List<CustomerAddressDto> Addresses { get; set; }
        public List<SalesOrder> SalesOrders { get; set; }
    }
}