using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrders
{
    public class CreditCardDto : IMapFrom<Entities.CreditCard>
    {
        public string CardType { get; set; }

        public string CardNumber { get; set; }

        public byte ExpMonth { get; set; }

        public short ExpYear { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.CreditCard, CreditCardDto>();
        }
    }
}