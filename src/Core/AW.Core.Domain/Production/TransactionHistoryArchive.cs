using System;

namespace AW.Core.Domain.Production
{
    public class TransactionHistoryArchive
    {
        public virtual int Id { get; protected set; }
        public int ProductID { get; set; }

        public int ReferenceOrderID { get; set; }

        public int ReferenceOrderLineID { get; set; }

        public DateTime TransactionDate { get; set; }
        
        public string TransactionType { get; set; }

        public int Quantity { get; set; }

        public decimal ActualCost { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}