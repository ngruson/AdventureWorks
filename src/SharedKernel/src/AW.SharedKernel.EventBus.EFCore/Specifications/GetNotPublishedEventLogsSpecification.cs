using Ardalis.Specification;
using AW.SharedKernel.EventBus.IntegrationEventLog;
using System.Linq;

namespace AW.SharedKernel.EventBus.EFCore.Specifications
{
    public class GetNotPublishedEventLogsSpecification : Specification<IntegrationEventLogEntry>
    {
        public GetNotPublishedEventLogsSpecification(string transactionId)
        {
            Query
                .Where(e => e.TransactionId == transactionId
                    && e.State == EventStateEnum.NotPublished);
        }
    }
}