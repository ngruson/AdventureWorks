using AW.Services.Infrastructure;
using System;
using System.Runtime.Serialization;

namespace AW.Services.Sales.Core.Exceptions
{
    [Serializable]
    public class SpecialOfferProductNotFoundException : DomainException
    {
        public SpecialOfferProductNotFoundException(string productNumber)
            : base($"Special offer for {productNumber} not found")
        { }

        protected SpecialOfferProductNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}