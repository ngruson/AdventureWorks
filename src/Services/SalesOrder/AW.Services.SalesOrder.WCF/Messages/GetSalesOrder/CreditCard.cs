using AutoMapper;
using AW.Services.SalesOrder.Application.Common;
using AW.Services.SalesOrder.Application.GetSalesOrder;

namespace AW.Services.SalesOrder.WCF.Messages.GetSalesOrder
{
    public class CreditCard : IMapFrom<CreditCardDto>
    {
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public byte ExpMonth { get; set; }
        public short ExpYear { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreditCardDto, CreditCard>();
        }
    }
}