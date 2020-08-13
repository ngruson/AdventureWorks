using System;

namespace AW.Domain.Production
{
    public class ScrapReason : BaseEntity
    {
        public short ScrapReasonID { get; set; }

        public string Name { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}