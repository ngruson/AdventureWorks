﻿using AW.Services.SalesOrder.Core.Handlers.CreateSalesOrder;
using AW.Services.SalesOrder.Core.Models;
using AW.SharedKernel.EventBus.Events;
using System;
using System.Collections.Generic;

namespace AW.Services.SalesOrder.Core.IntegrationEvents.Events
{
#if NET
    public record UserCheckoutAcceptedIntegrationEvent : IntegrationEvent
#elif NETSTANDARD
    public class UserCheckoutAcceptedIntegrationEvent : IntegrationEvent
#endif
    {
        public string UserId { get; }
        public string UserName { get; }
        public string CustomerNumber { get; set; }
        public string ShipMethod { get; set; }
        public Address BillToAddress { get; set; }
        public Address ShipToAddress { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime CardExpiration { get; set; }
        public string CardSecurityNumber { get; set; }
        public string CardType { get; set; }
        public string Buyer { get; set; }
        public Guid RequestId { get; set; }
        public List<SalesOrderItemDto> BasketItems { get; }

        public UserCheckoutAcceptedIntegrationEvent(string userId, string userName, 
            Address billToAddress, Address shipToAddress, 
            string cardNumber, string cardHolderName,
            DateTime cardExpiration, string cardSecurityNumber, string cardType, string buyer, Guid requestId,
            List<SalesOrderItemDto> basketItems)
        {
            UserId = userId;
            BillToAddress = billToAddress;
            ShipToAddress = shipToAddress;
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            CardExpiration = cardExpiration;
            CardSecurityNumber = cardSecurityNumber;
            CardType = cardType;
            Buyer = buyer;
            BasketItems = basketItems;
            RequestId = requestId;
            UserName = userName;
        }
    }
}