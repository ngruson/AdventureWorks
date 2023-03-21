using System.Runtime.Serialization;
using AW.Services.Infrastructure;

namespace AW.Services.Product.Core.Exceptions
{
    [Serializable]
    public class ProductSubcategoryNotFoundException : DomainException
    {
        public ProductSubcategoryNotFoundException(string name)
            : base($"Product subcategory {name} not found")
        { }

        protected ProductSubcategoryNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
