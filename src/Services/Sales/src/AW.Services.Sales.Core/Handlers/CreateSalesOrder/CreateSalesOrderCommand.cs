using MediatR;
using System;
using System.Collections.Generic;

namespace AW.Services.Sales.Core.Handlers.CreateSalesOrder
{
    public class CreateSalesOrderCommand : IRequest<bool>
    {
        public CreateSalesOrderCommand() { }
        public CreateSalesOrderCommand(List<SalesOrderItemDto> items, string userId, string userName,
            string customerNumber, string shipMethod,
            AddressDto billToAddress, AddressDto shipToAddress,
            string cardNumber, string cardHolderName, DateTime cardExpiration,
            string cardSecurityNumber, string cardType) : this()
        {
            OrderItems = items;
            UserId = userId;
            UserName = userName;
            CustomerNumber = customerNumber;
            ShipMethod = shipMethod;
            BillToAddress = billToAddress;
            ShipToAddress = shipToAddress;
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            CardExpiration = cardExpiration;
            CardSecurityNumber = cardSecurityNumber;
            CardType = cardType;
            CardExpiration = cardExpiration;
        }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string CustomerNumber { get; set; }
        public string ShipMethod { get; set; }
        public AddressDto BillToAddress { get; set; }
        public AddressDto ShipToAddress { get; set; }

        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime CardExpiration { get; set; }
        public string CardSecurityNumber { get; set; }
        public string CardType { get; set; }
        public List<SalesOrderItemDto> OrderItems { get; set; }
    }
}