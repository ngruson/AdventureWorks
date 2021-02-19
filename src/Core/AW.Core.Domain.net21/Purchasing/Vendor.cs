using AW.Core.Domain.Person;
using System;
using System.Collections.Generic;

namespace AW.Core.Domain.Purchasing
{    
    public class Vendor
    {
        public virtual int Id { get; protected set; }
        public string AccountNumber { get; set; }
        
        public string Name { get; set; }

        public byte CreditRating { get; set; }

        public bool PreferredVendorStatus { get; set; }

        public bool ActiveFlag { get; set; }
        
        public string PurchasingWebServiceURL { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual BusinessEntity BusinessEntity { get; set; }

        public virtual ICollection<ProductVendor> ProductVendor { get; set; } = new List<ProductVendor>();
    }
}