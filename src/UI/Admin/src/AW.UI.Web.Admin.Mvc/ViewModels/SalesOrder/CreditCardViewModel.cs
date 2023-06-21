using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder;

public class CreditCardViewModel : IMapFrom<Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder.CreditCard>
{
    public string? CardType { get; set; }

    public string? CardNumber { get; set; }

    public byte ExpMonth { get; set; }

    public short ExpYear { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders.CreditCard, CreditCardViewModel>()
            .ForMember(m => m.CardNumber, opt => opt.MapFrom(src => MaskCardNumber(src.CardNumber!)));

        profile.CreateMap<Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder.CreditCard, CreditCardViewModel>()
            .ForMember(m => m.CardNumber, opt => opt.MapFrom(src => MaskCardNumber(src.CardNumber!)));
    }

    private static string MaskCardNumber(string cardNumber)
    {
        if (cardNumber.Length < 4)
            return cardNumber;

        string maskPart = cardNumber[..^4];
        return cardNumber[maskPart.Length..].PadLeft(cardNumber.Length, '*');
    }
}
