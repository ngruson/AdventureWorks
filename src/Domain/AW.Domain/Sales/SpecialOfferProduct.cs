using AW.Domain.Production;
using System;

namespace AW.Domain.Sales
{
    public partial class SpecialOfferProduct
    {
        public int SpecialOfferID { get; protected set; }

        public int ProductID { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual Product Product { get; set; }

        public virtual SpecialOffer SpecialOffer { get; set; }
    }
}