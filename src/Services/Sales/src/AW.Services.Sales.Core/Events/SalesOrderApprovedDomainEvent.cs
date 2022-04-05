using AW.Services.Sales.Core.Entities;
using MediatR;

namespace AW.Services.Sales.Core.Events
{
    public class SalesOrderApprovedDomainEvent : INotification
    {
        public SalesOrder SalesOrder { get; }
        public SalesOrderApprovedDomainEvent(SalesOrder salesOrder)
        {
            SalesOrder = salesOrder;
        }
    }
}