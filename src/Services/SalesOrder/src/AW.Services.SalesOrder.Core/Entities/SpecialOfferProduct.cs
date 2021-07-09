namespace AW.Services.SalesOrder.Core.Entities
{
    public partial class SpecialOfferProduct
    {
        public int Id { get; set; }
        public int SpecialOfferID { get; set; }
        public SpecialOffer SpecialOffer { get; set; }
        public string ProductNumber { get; set; }
        
    }
}