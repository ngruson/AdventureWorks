namespace AW.Core.Abstractions.Api.CustomerApi.UpdateCustomer
{
    public class Customer
    {
        public string AccountNumber { get; set; }
        public string SalesTerritoryName { get; set; }
        public Person Person { get; set; }
        public Store Store { get; set; }
    }
}