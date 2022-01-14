using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Models
{
    public class CreditCard : IMapFrom<Handlers.GetSalesOrders.CreditCardDto>
    {
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public byte ExpMonth { get; set; }
        public short ExpYear { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Handlers.GetSalesOrders.CreditCardDto, CreditCard>();
            profile.CreateMap<Handlers.GetSalesOrder.CreditCardDto, CreditCard>();
        }
    }
}