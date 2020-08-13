using System;

namespace AW.Domain.Production
{
    public class ProductModelIllustration : BaseEntity
    {
        public int IllustrationID { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual Illustration Illustration { get; set; }
    }
}