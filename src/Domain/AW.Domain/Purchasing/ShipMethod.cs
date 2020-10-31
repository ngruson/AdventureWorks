using System;

namespace AW.Domain.Purchasing
{
    public class ShipMethod
    {
        public virtual int Id { get; protected set; }
        public string Name { get; set; }
        
        public decimal ShipBase { get; set; }

        public decimal ShipRate { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}