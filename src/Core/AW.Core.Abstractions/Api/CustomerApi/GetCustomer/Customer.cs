using System.Collections.Generic;

namespace AW.Core.Abstractions.Api.CustomerApi.GetCustomer
{
    public class Customer
    {
        public string AccountNumber { get; set; }
        public string SalesTerritoryName { get; set; }
        public Person Person { get; set; }
        public Store Store { get; set; }
        public List<SalesOrder> SalesOrders { get; set; }
    }
}