using System;

namespace AW.Core.Domain.Sales
{
    public partial class SalesReason
    {
        public virtual int Id { get; set; }

        public string Name { get; set; }

        public string ReasonType { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}