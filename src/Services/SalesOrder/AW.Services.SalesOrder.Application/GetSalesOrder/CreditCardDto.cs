using AutoMapper;
using AW.Common.AutoMapper;

namespace AW.Services.SalesOrder.Application.GetSalesOrder
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