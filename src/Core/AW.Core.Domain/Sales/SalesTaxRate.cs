using AW.Core.Domain.Person;
using System;

namespace AW.Core.Domain.Sales
{
    public partial class SalesTaxRate
    {
        public virtual int Id { get; protected set; }

        public int StateProvinceID { get; set; }

        public byte TaxType { get; set; }

        public decimal TaxRate { get; set; }

        public string Name { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual StateProvince StateProvince { get; set; }
    }
}