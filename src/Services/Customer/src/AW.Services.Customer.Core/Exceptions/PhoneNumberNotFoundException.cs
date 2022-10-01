using AW.Services.Infrastructure;
using System;
using System.Runtime.Serialization;

namespace AW.Services.Customer.Core.Exceptions
{
    [Serializable]
    public class PhoneNumberNotFoundException : DomainException
    {
        public PhoneNumberNotFoundException(string accountNumber, string phoneNumber, string phoneNumberType)
            : base($"Phone number (number: {phoneNumber}, type: {phoneNumberType}) for customer {accountNumber} not found")
        { }

        protected PhoneNumberNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}