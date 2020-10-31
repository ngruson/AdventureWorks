using System;

namespace AW.Domain.Sales
{
    public partial class SalesReason
    {
        public virtual int Id { get; protected set; }

        public string Name { get; set; }

        public string ReasonType { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}