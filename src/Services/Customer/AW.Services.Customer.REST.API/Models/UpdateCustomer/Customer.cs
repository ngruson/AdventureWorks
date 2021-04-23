using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AW.Services.Customer.REST.API.Models.UpdateCustomer
{
    public abstract class Customer
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public abstract CustomerType CustomerType { get; }

        public string Territory { get; set; }
        public List<CustomerAddress> Addresses { get; set; }
    }
}