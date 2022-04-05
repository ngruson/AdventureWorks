using AW.Services.Sales.Core.Entities;
using MediatR;

namespace AW.Services.Sales.Core.Events
{
    public class SalesOrderShippedDomainEvent : INotification
    {
        public SalesOrder SalesOrder { get; set; }

        public SalesOrderShippedDomainEvent(SalesOrder salesOrder)
        {
            this.SalesOrder = salesOrder;
        }
    }
}