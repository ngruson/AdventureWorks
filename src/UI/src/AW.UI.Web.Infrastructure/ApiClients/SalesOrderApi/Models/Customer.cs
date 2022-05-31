using AW.SharedKernel.Interfaces;
using System.Text.Json.Serialization;

namespace AW.UI.Web.Infrastructure.ApiClients.SalesOrderApi.Models
{
    public abstract class Customer : ICustomer
    {
        public AW.SharedKernel.Interfaces.CustomerType CustomerType { get; set; }
        public string CustomerNumber { get; set; }
        
        [JsonIgnore]
        public virtual string CustomerName { get; }
    }
}