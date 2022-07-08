using System;
using System.Runtime.Serialization;

namespace AW.UI.Web.Infrastructure.ApiClients
{
    [Serializable]
    public class CustomerApiClientException : Exception
    {
        public CustomerApiClientException()
        { }

        protected CustomerApiClientException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        public CustomerApiClientException(string message)
            : base(message)
        { }

        public CustomerApiClientException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}