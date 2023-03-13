namespace AW.Services.Product.Core.Entities
{
    public class ProductModelIllustration
    {
        public int ProductModelID { get; set; }
        public int IllustrationID { get; set; }

        public Illustration? Illustration { get; private set; }
    }
}
