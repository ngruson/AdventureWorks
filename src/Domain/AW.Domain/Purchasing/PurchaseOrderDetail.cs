using AW.Domain.Production;
using System;

namespace AW.Domain.Purchasing
{    
    public partial class PurchaseOrderDetail
    {
        public int PurchaseOrderID { get; set; }

        public int PurchaseOrderDetailID { get; set; }

        public DateTime DueDate { get; set; }

        public short OrderQty { get; set; }

        public int ProductID { get; set; }
        
        public decimal UnitPrice { get; set; }
        
        public decimal LineTotal { get; set; }

        public decimal ReceivedQty { get; set; }

        public decimal RejectedQty { get; set; }
        
        public decimal StockedQty { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual Product Product { get; set; }

        public virtual PurchaseOrderHeader PurchaseOrderHeader { get; set; }
    }
}