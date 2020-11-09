using System;
using System.Runtime.Serialization;

namespace AW.Application.Exceptions
{
    [Serializable]
    public class AddressTypesNotFoundException : ApplicationException
    {
        public AddressTypesNotFoundException() : base($"No address types found")
        { 
        }

        protected AddressTypesNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}