using AW.Common.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AW.Services.Customer.REST.API.Models.UpdateCustomer
{
    public abstract class Customer : ICustomer
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CustomerType CustomerType { get; set; }

        public string Territory { get; set; }
        public List<CustomerAddress> Addresses { get; set; }
    }
}