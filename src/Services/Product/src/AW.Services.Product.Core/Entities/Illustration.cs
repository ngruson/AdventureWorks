namespace AW.Services.Product.Core.Entities
{
    public class Illustration
    {
        public int Id { get; set; }
        public string? Diagram { get; private set; }

        public DateTime ModifiedDate { get; private set; }        
    }
}