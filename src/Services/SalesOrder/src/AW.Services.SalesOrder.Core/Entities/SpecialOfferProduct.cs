using AW.SharedKernel.Interfaces;

namespace AW.Services.SalesOrder.Core.Entities
{
    public class SpecialOfferProduct : IAggregateRoot
    {
        public int Id { get; set; }
        public int SpecialOfferId { get; set; }
        public SpecialOffer SpecialOffer { get; set; }
        public string ProductNumber { get; set; }
        
    }
}