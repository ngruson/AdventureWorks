using AutoMapper;
using AW.Services.Sales.Core.Entities;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrdersForCustomer
{
    public class CreditCardDto : IMapFrom<CreditCard>
    {
        public string CardType { get; set; }

        public string CardNumber { get; set; }

        public byte ExpMonth { get; set; }

        public short ExpYear { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreditCard, CreditCardDto>();
        }
    }
}