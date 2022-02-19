using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.SharedKernel.Interfaces;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AW.SharedKernel.JsonConverters
{
    public class CustomerConverter<T, TStore, TIndividual> : JsonConverter<T>
        where T : class, ICustomer
        where TStore : class, T
        where TIndividual : class, T
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null) return null;
            var readerCopy = reader;

            // Read the `className` from our JSON document
            using var jsonDocument = JsonDocument.ParseValue(ref readerCopy);
            var jsonObject = jsonDocument.RootElement;

            var customerType = jsonObject.GetProperty("customerType");
            Guard.Against.Null(customerType, nameof(customerType));

            if (!string.IsNullOrEmpty(customerType.GetString()))
            {
                if (Enum<CustomerType>.Parse(customerType.GetString()) == CustomerType.Store)
                    return JsonSerializer.Deserialize(ref reader, typeof(TStore), options) as TStore;

                if (Enum<CustomerType>.Parse(customerType.GetString()) == CustomerType.Individual)
                    return JsonSerializer.Deserialize(ref reader, typeof(TIndividual), options) as TIndividual;
            }

            throw new NotSupportedException($"{customerType.GetString() ?? "<unknown>"} can not be deserialized");
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case null:
                    JsonSerializer.Serialize(writer, (T)null, options);
                    break;
                default:
                    {
                        JsonSerializer.Serialize(writer, value, value.GetType(), options);
                        break;
                    }
            }
        }
    }
}