using AW.Services.SharedKernel.ValueTypes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AW.Services.SharedKernel.JsonConverters
{
    public class EmailAddressConverter : JsonConverter<EmailAddress>
    {
        public override EmailAddress? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null) return null;
            var readerCopy = reader;

            using var jsonDocument = JsonDocument.ParseValue(ref readerCopy);
            var emailAddress = jsonDocument.RootElement.ToString();

            return EmailAddress.Create(emailAddress).Value;
        }

        public override void Write(Utf8JsonWriter writer, EmailAddress value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }
    }
}
