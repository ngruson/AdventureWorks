namespace AW.Services.SalesOrder.Domain
{
    public class StoreCustomerContact
    {
        public int Id { get; set; }
        public string ContactType { get; set; }
        public Person ContactPerson { get; set; }
    }
}