namespace AW.Services.Product.Core.Entities
{
    public class ProductModelIllustration
    {
        public int ProductModelID { get; set; }
        public int IllustrationID { get; set; }

        public DateTime ModifiedDate { get; private set; }

        public Illustration? Illustration { get; private set; }
    }
}