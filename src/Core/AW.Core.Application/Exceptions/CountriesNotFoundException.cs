using System;
using System.Runtime.Serialization;

namespace AW.Core.Application.Exceptions
{
    [Serializable]
    public class CountriesNotFoundException : ApplicationException
    {
        public CountriesNotFoundException() : base($"No countries found")
        { 
        }

        protected CountriesNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}