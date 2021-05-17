using AutoMapper;
using AW.Common.AutoMapper;

namespace AW.Services.SalesOrder.REST.API.Models
{
    public class CreditCard : IMapFrom<Application.GetSalesOrders.CreditCardDto>
    {
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public byte ExpMonth { get; set; }
        public short ExpYear { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Application.GetSalesOrders.CreditCardDto, CreditCard>();
            profile.CreateMap<Application.GetSalesOrder.CreditCardDto, CreditCard>();
        }
    }
}