using AW.Services.Sales.Core.Entities;
using MediatR;

namespace AW.Services.Sales.Core.Events
{
    public class SalesOrderCancelledDomainEvent : INotification
    {
        public SalesOrder SalesOrder { get; }
        public SalesOrderCancelledDomainEvent(SalesOrder salesOrder)
        {
            SalesOrder = salesOrder;
        }
    }
}