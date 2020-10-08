using System;
using System.Runtime.Serialization;

namespace AW.Application.Exceptions
{
    [Serializable]
    public class CustomerNotFoundException : ApplicationException
    {
        public CustomerNotFoundException() { }

        public CustomerNotFoundException(string accountNumber) : base($"Customer \"{accountNumber}\" not found.")
        {
        }

        protected CustomerNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}