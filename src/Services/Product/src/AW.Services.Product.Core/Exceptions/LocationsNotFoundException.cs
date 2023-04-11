using System.Runtime.Serialization;
using AW.Services.Infrastructure;

namespace AW.Services.Product.Core.Exceptions
{
    [Serializable]
    public class LocationsNotFoundException : DomainException
    {
        public LocationsNotFoundException()
            : base($"Locations not found")
        { }

        protected LocationsNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
