using AutoMapper;
using AW.Services.SalesOrder.Application.Common;

namespace AW.Services.SalesOrder.Application.GetSalesOrders
{
    public class CreditCardDto : IMapFrom<Domain.CreditCard>
    {
        public string CardType { get; set; }

        public string CardNumber { get; set; }

        public byte ExpMonth { get; set; }

        public short ExpYear { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.CreditCard, CreditCardDto>();
        }
    }
}