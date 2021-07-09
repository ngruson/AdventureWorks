﻿using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.SalesOrder.REST.API.Models
{
    public class CreditCard : IMapFrom<Core.Handlers.GetSalesOrders.CreditCardDto>
    {
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public byte ExpMonth { get; set; }
        public short ExpYear { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Handlers.GetSalesOrders.CreditCardDto, CreditCard>();
            profile.CreateMap<Core.Handlers.GetSalesOrder.CreditCardDto, CreditCard>();
        }
    }
}