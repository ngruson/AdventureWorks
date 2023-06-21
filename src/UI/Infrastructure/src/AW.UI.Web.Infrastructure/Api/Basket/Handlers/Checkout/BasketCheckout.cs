namespace AW.UI.Web.Infrastructure.Api.Basket.Handlers.Checkout;

public class BasketCheckout
{
    public string? CustomerNumber { get; set; }
    public string? ShipMethod { get; set; }
    public Address? BillToAddress { get; set; }
    public Address? ShipToAddress { get; set; }

    public string? CardNumber { get; set; }

    public string? CardHolderName { get; set; }

    public DateTime CardExpiration { get; set; }

    public string? CardSecurityNumber { get; set; }

    public string? CardType { get; set; }

    public string? Buyer { get; set; }

    public Guid RequestId { get; set; }
    public List<BasketItem>? Items { get; set; }
}
