using AW.SharedKernel.Interfaces;
using System.Text.Json.Serialization;

namespace AW.Services.Customer.Core.Models.UpdateCustomer
{
    public abstract class Customer : ICustomer
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CustomerType CustomerType { get; set; }
        public string? AccountNumber { get; set; }
        public string? Territory { get; set; }
        public List<CustomerAddress> Addresses { get; set; } = new(); 
    }
}