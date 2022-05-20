using AW.SharedKernel.Interfaces;
using System.Text.Json.Serialization;

namespace AW.Services.Sales.Core.Models
{
    public abstract class Customer : ICustomer
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CustomerType CustomerType { get; set; }

        public string CustomerNumber { get; set; }
    }
}