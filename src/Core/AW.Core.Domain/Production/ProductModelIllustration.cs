using System;

namespace AW.Core.Domain.Production
{
    public class ProductModelIllustration
    {
        public int ProductModelID { get; set; }
        public int IllustrationID { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual Illustration Illustration { get; set; }
    }
}