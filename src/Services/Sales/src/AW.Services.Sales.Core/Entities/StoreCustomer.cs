namespace AW.Services.Sales.Core.Entities
{
    public class StoreCustomer : Customer
    {
        public override CustomerType CustomerType => CustomerType.Store;
        public string? Name { get; set; }
        public SalesPerson? SalesPerson { get; set; }

        public override string? FullName => Name;
    }
}