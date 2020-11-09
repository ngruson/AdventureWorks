using System;
using System.Runtime.Serialization;

namespace AW.Application.Exceptions
{
    [Serializable]
    public class ContactTypesNotFoundException : ApplicationException
    {
        public ContactTypesNotFoundException() : base($"No contact types found")
        { 
        }

        protected ContactTypesNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}