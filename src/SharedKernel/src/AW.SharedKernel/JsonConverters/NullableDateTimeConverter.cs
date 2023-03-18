using System.Text.Json;
using System.Text.Json.Serialization;

namespace AW.SharedKernel.JsonConverters
{
    public class NullableDateTimeConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var stringValue = reader.GetString();
                if (string.IsNullOrEmpty(stringValue))
                    return null;

                if (DateTime.TryParse(stringValue, out DateTime value))
                {
                    return value;
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {            
            if (value.HasValue)
                writer.WriteStringValue(value.Value);
        }
    }
}
