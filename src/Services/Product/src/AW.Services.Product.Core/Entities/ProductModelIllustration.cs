using System;

namespace AW.Services.Product.Core.Entities
{
    public class ProductModelIllustration
    {
        private int ProductModelID { get; set; }
        private int IllustrationID { get; set; }

        public DateTime ModifiedDate { get; private set; }

        public Illustration Illustration { get; private set; }
    }
}