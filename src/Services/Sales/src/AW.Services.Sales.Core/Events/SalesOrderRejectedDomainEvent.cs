using AW.Services.Sales.Core.Entities;
using MediatR;

namespace AW.Services.Sales.Core.Events
{
    public class SalesOrderRejectedDomainEvent : INotification
    {
        public SalesOrder SalesOrder { get; set; }
        public SalesOrderRejectedDomainEvent(SalesOrder salesOrder)
        {
            SalesOrder = salesOrder;
        }
    }
}