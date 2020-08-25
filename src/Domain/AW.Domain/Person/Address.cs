using System;

namespace AW.Domain.Person
{
    public class Address : BaseEntity
    {
        public string AddressLine1 { get; set; }
        
        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public int StateProvinceID { get; set; }
        
        public string PostalCode { get; set; }

        //public string SpatialLocation { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual StateProvince StateProvince { get; set; }
    }
}