using AW.SharedKernel.EventBus.Events;

namespace AW.Services.Basket.Core.IntegrationEvents.Events
{
    public record ProductPriceChangedIntegrationEvent : IntegrationEvent
    {
        public string ProductNumber { get; private init; }

        public decimal NewPrice { get; private init; }

        public decimal OldPrice { get; private init; }

        public ProductPriceChangedIntegrationEvent(string productNumber, decimal newPrice, decimal oldPrice)
        {
            ProductNumber = productNumber;
            NewPrice = newPrice;
            OldPrice = oldPrice;
        }
    }
}